using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Services;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Zenject;

// Slot Script

public enum SlotState
{
    Draggable,
    Unloading,
    NonTouchable,
    Blocked,
}


public class Slot : MonoBehaviour, IDropHandler
{
    [Inject] private IPlayerProgressService PlayerProgressService;

    [SerializeField] private Image _itemImage;
    [SerializeField] private SlotState _slotState;
    [SerializeField] private MergeItem _item;
    [SerializeField] private Image _stateImage;
    private Slot[] _neighbours;

    public SlotState SlotState { get => _slotState; }

    public delegate void SlotDelegate();
    public SlotDelegate addItemEvent;
    public SlotDelegate removeItemEvent;
    public SlotDelegate saveEvent;

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
        if(_item.itemSprite != null)
        {
            _itemImage.enabled = true;
            _itemImage.sprite = _item.itemSprite;
        }

        addItemEvent?.Invoke();
        if (isNeedSave)
            saveEvent?.Invoke();
    }

    public void RemoveItem(bool isNeedSave = false)
    {
        _item = null;
        _itemImage.enabled = false;

        removeItemEvent?.Invoke();
        if(isNeedSave)
            saveEvent?.Invoke();
    }

    private void UpgradeItem()
    {
        MergeItem newItem = _item.nextItem;
        RemoveItem();
        AddItem(newItem);
    }

    public void SetNeighbours(Slot[] neighbours)
    {
        _neighbours = neighbours;
    }
    private void CheckNeighbour()
    {
        if (_slotState == SlotState.NonTouchable)
            ChangeState(SlotState.Draggable);

        foreach(Slot neighbour in _neighbours)
        {
            if (neighbour.SlotState == SlotState.Blocked)
                neighbour.ChangeState(SlotState.NonTouchable);
        }
    }

    public DraggableItem currentDraggableItem() 
    { 
        return GetComponentInChildren<DraggableItem>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (_slotState == SlotState.Blocked 
            || _slotState == SlotState.Unloading)
        {
            return;
        }

        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (draggableItem.slot == this 
            || draggableItem.slot._slotState != SlotState.Draggable)
            return;

        ChangeSlot(draggableItem.slot);
        OnClick();
        currentDraggableItem().isClicked = true;
    }

    private void ChangeSlot(Slot fromSlot)
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
            saveEvent?.Invoke();
            return;
        }

        //3 move
        if (IsEmpty)
        {
            CheckNeighbour();
            fromSlot.RemoveItem();
            AddItem(droppedItem);
            saveEvent?.Invoke();
            return;
        }

        //4 no move
        if (_slotState == SlotState.NonTouchable)
            return;

        //5 switch
        CheckNeighbour();
        MergeItem toItem = CurrentItem;
        RemoveItem();
        AddItem(droppedItem);
        fromSlot.RemoveItem();
        fromSlot.AddItem(toItem);
        saveEvent?.Invoke();
    }

    private void MergeItems(Slot slotFrom, Slot slotTo)
    {
        if (slotFrom.CurrentItem.nextItem == null)
            return;

        if (slotFrom.CurrentItem != slotTo.CurrentItem)
            return;

        PlayerProgressService.Progress.AddCoins(10);

        slotFrom.RemoveItem();
        slotTo.UpgradeItem();
    }

    public void OnClick()
    {
        if (!IsEmpty)
        {
            if (SlotState == SlotState.Blocked)
            {
                return;
            }
            transform.parent.GetComponent<MergeGrid>().informationPanel.ConfigPanel(this);

            if (currentDraggableItem().isClicked)
            {
                CurrentItem.UseItem();
            }
            else
            {
                currentDraggableItem().isClicked = true;
            }
        }
        else
        {
            transform.parent.GetComponent<MergeGrid>().informationPanel.ActivateInfromPanel(false);
            currentDraggableItem().isClicked = false;
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

        _stateImage.enabled = false;
        switch (_slotState)
        {
            case SlotState.Blocked:
                _stateImage.enabled = true;
                StartCoroutine(LoadImage(AssetName.BlockedSlot, _stateImage));
                break;
            case SlotState.Draggable:
                break;
            case SlotState.Unloading:
                Image backgroundImage = GetComponent<Image>();
                StartCoroutine(LoadImage(AssetName.DeliveryZone, backgroundImage));
                break;
            case SlotState.NonTouchable:
                _stateImage.enabled = true;
                StartCoroutine(LoadImage(AssetName.NonTouchableSlot, _stateImage));
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
        currentDraggableItem().isClicked = false;
    }

    public void UseItemInside()
    {
        if (!IsEmpty)
        {
            CurrentItem.UseItem();
        }
    }
}
