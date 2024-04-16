using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Zenject;

public enum SlotState
{
    Draggable,
    Unloading,
    NonTouchable,
    Blocked,
}

public class SlotRenderer : MonoBehaviour, IDropHandler
{
    [Inject] private PlayerProgressService _playerProgressService;
    [Inject] private SlotsManager _slotsManager;
    [Inject] private InformationPanel _informationPanel;
    [Inject] private AudioPlayer _audio;

    [SerializeField] private Image _itemImage;
    [SerializeField] private SlotState _slotState;
    [SerializeField] private MergeItem _item;
    [SerializeField] private GameObject _slotStatePrefab;
    private SlotRenderer[] _neighbours;

    private const string SLOT_STATE_NAME = "SlotState(Clone)";

    public SlotState SlotState
    {
        get => _slotState;
    }

    public delegate void SlotDelegate();

    public SlotDelegate addItemEvent;
    public SlotDelegate removeItemEvent;
    public SlotDelegate endMoveEvent;

    public bool IsEmpty => _item == null;
    public bool IsOpen => _slotState == SlotState.Draggable;
    public MergeItem CurrentItem => _item;


    private void Start()
    {
        CheckState();
    }

    public void AddItem(MergeItem newItem, bool isNeedSave = false)
    {
        if (newItem == null)
            return;

        _item = newItem;
        if (_item.ItemSprite != null)
        {
            _itemImage.enabled = true;
            _itemImage.sprite = _item.ItemSprite;
        }

        addItemEvent?.Invoke();
        if (isNeedSave)
            endMoveEvent?.Invoke();
    }

    public void RemoveItem(bool isNeedSave = false)
    {
        _item = null;
        _itemImage.enabled = false;

        removeItemEvent?.Invoke();
        if (isNeedSave)
            endMoveEvent?.Invoke();
    }

    private void UpgradeItem()
    {
        MergeItem newItem = _item.nextItem;
        RemoveItem();
        AddItem(newItem);
    }

    public void SetNeighbours(SlotRenderer[] neighbours)
    {
        _neighbours = neighbours;
    }

    private void CheckNeighbour()
    {
        if (_slotState == SlotState.Unloading)
            return;

        if (_slotState == SlotState.NonTouchable)
            ChangeState(SlotState.Draggable);

        if (_neighbours == null || _neighbours.Length == 0)
            return;

        foreach (SlotRenderer neighbour in _neighbours)
        {
            if (neighbour.SlotState == SlotState.Blocked)
                neighbour.ChangeState(SlotState.NonTouchable);
        }
    }

    public DraggableItem RurrentDraggableItem()
    {
        return GetComponentInChildren<DraggableItem>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (_slotState == SlotState.Blocked)
            return;

        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (draggableItem.slot == this)
            return;

        ChangeSlot(draggableItem.slot);
        OnClick();
        RurrentDraggableItem().isClicked = true;
    }

    private void ChangeSlot(SlotRenderer fromSlot)
    {
        MergeItem droppedItem = fromSlot.CurrentItem;

        //1 no item
        if (droppedItem == null)
            return;

        //2 merge
        if (CurrentItem == droppedItem)
        {
            CheckNeighbour();
            MergeItems(fromSlot, this);
            endMoveEvent?.Invoke();
            return;
        }

        //3 move
        if (IsEmpty && _slotState != SlotState.Unloading)
        {
            fromSlot.RemoveItem();
            AddItem(droppedItem);
            endMoveEvent?.Invoke();
            return;
        }

        //4 no move
        if (_slotState == SlotState.NonTouchable)
            return;

        //5 switch
        if (_slotState != SlotState.Draggable
            || fromSlot._slotState != SlotState.Draggable)
            return;
        MergeItem toItem = CurrentItem;
        RemoveItem();
        AddItem(droppedItem);
        fromSlot.RemoveItem();
        fromSlot.AddItem(toItem);
        endMoveEvent?.Invoke();
    }

    private void MergeItems(SlotRenderer slotFrom, SlotRenderer slotTo)
    {
        if (slotFrom.CurrentItem.nextItem == null)
            return;

        if (slotFrom.CurrentItem != slotTo.CurrentItem)
            return;

        _audio.PlayMergeSound(slotFrom.CurrentItem.ItemLevel);

        _playerProgressService.AddCoins(10);

        slotFrom.RemoveItem();
        slotTo.UpgradeItem();
    }

    public void OnClick()
    {
        DraggableItem item = RurrentDraggableItem();
        if (item == null) return;
        
        if (!IsEmpty)
        {
            if (SlotState == SlotState.Blocked
                || SlotState == SlotState.NonTouchable)
                return;

            _informationPanel.ConfigPanel(this);

            if (item.isClicked)
                UseItemInside();
            else
                item.isClicked = true;
        }
        else
        {
            _informationPanel.ActivateInfromPanel(false);
            item.isClicked = false;
        }
    }

    public void ChangeState(SlotState m_slotState)
    {
        _slotState = m_slotState;
        CheckState();
    }

    private void CheckState()
    {
        if (!isActiveAndEnabled)
            return;

        Transform slotState = transform.Find(SLOT_STATE_NAME);
        Image slotStateImage = null;
        if (slotState != null)
            slotStateImage = slotState.GetComponent<Image>();


        switch (_slotState)
        {
            case SlotState.Blocked:
                if (slotStateImage == null)
                    slotStateImage = Instantiate(_slotStatePrefab, transform).GetComponent<Image>();
                StartCoroutine(LoadImage(AssetName.BlockedSlot, slotStateImage));
                break;
            case SlotState.Draggable:
                if (slotStateImage != null)
                    Destroy(slotStateImage.gameObject);
                break;
            case SlotState.Unloading:
                Image backgroundImage = GetComponent<Image>();
                StartCoroutine(LoadImage(AssetName.DeliveryZone, backgroundImage));
                break;
            case SlotState.NonTouchable:
                if (slotStateImage == null)
                    slotStateImage = Instantiate(_slotStatePrefab, transform).GetComponent<Image>();
                StartCoroutine(LoadImage(AssetName.NonTouchableSlot, slotStateImage));
                break;
        }
    }

    public IEnumerator LoadImage(string imageKey, Image image)
    {
        AsyncOperationHandle<Sprite> opHandle = Addressables.LoadAssetAsync<Sprite>(imageKey);
        yield return opHandle;

        if (opHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to load a sprite resource named \"{imageKey}\"!");
            yield break;
        }

        image.sprite = opHandle.Result;
    }

    public void DisableSelected()
    {
        RurrentDraggableItem().isClicked = false;
    }

    public void UseItemInside()
    {
        if (IsEmpty)
            return;

        if (CurrentItem.InItemsCount == 0)
            return;

        MergeItem item = CurrentItem.InItem;
        _slotsManager.AddItemToEmptySlot(item);
    }
}