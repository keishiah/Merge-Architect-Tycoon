using System;
using System.Collections;
using UniRx;
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

public class Slot : MonoBehaviour, IDropHandler
{
    [Inject] private IPlayerProgressService _playerProgressService;
    [Inject] private SlotsManager _slotsManager;

    [SerializeField] private Image _itemImage;
    [SerializeField] private SlotState _slotState;
    [SerializeField] private MergeItem _item;
    [SerializeField] private Image _stateImage;
    private Slot[] _neighbours;

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
        if (_item.itemSprite != null)
        {
            _itemImage.enabled = true;
            _itemImage.sprite = _item.itemSprite;
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

    public void SetNeighbours(Slot[] neighbours)
    {
        _neighbours = neighbours;
    }

    private void CheckNeighbour()
    {
        if (_slotState == SlotState.NonTouchable)
            ChangeState(SlotState.Draggable);

        foreach (Slot neighbour in _neighbours)
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
        if (_slotState == SlotState.Blocked)
            return;

        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (draggableItem.slot == this)
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

    private void MergeItems(Slot slotFrom, Slot slotTo)
    {
        if (slotFrom.CurrentItem.nextItem == null)
            return;

        if (slotFrom.CurrentItem != slotTo.CurrentItem)
            return;

        _playerProgressService.Progress.AddCoins(10);

        slotFrom.RemoveItem();
        slotTo.UpgradeItem();

        _playerProgressService.Progress.AddMergeItem();
    }

    public void OnClick()
    {
        if (!IsEmpty)
        {
            if (SlotState == SlotState.Blocked)
                return;

            transform.parent.GetComponent<MergeGrid>().informationPanel.ConfigPanel(this);

            if (currentDraggableItem().isClicked)
                UseItemInside();
            else
                currentDraggableItem().isClicked = true;
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
        if (IsEmpty)
            return;

        if (CurrentItem.InItemsCount == 0)
            return;

        MergeItem item = CurrentItem.InItem;
        _slotsManager.AddItemToEmptySlot(item);
    }
}