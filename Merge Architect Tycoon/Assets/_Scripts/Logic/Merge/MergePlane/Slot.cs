using CodeBase.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

// Slot Script

public enum SlotState
{
    Draggable,
    NonTouchable,
    Blocked,
}


public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image endLevelItemImage;
    [SerializeField] private int slotID;
    [SerializeField] private SlotState slotState;

    [SerializeField] private MergeItem item;
    [SerializeField] private int count;

    [Space, Header("State Images")]
    public Image blockedImage;
    public Image nontouchableImage;

    private Slot[] neighbours;

    public int SlotID { get => slotID; set => slotID = value; }
    public SlotState SlotState { get => slotState; }

    //Delegates
    public delegate void SlotDelegate();
    public SlotDelegate addItemEvent;
    public SlotDelegate removeItemEvent;
    public SlotDelegate saveEvent;

    public bool IsEmpty => item == null && slotState != SlotState.Blocked;

    public MergeItem CurrentItem
    {
        get { return item; }
    }

    [Inject] private IPlayerProgressService PlayerProgressService;

    public void AddItem(MergeItem newItem)
    {
        if (newItem == null)
            return;

        item = newItem;
        if(item.itemSprite != null)
            itemImage.sprite = item.itemSprite;
        itemImage.color = Color.white;

        if (!item.nextItem)
            endLevelItemImage.enabled = true;

        addItemEvent?.Invoke();
    }

    public void RemoveItem()
    {
        item = null;
        itemImage.sprite = null;
        endLevelItemImage.enabled = false;
        itemImage.color = Color.clear;

        removeItemEvent?.Invoke();
    }

    private void UpgradeItem()
    {
        MergeItem newItem = item.nextItem;
        RemoveItem();
        AddItem(newItem);
    }

    public void SetNeighbours(Slot[] neighbours)
    {
        this.neighbours = neighbours;
    }
    private void CheckNeighbour()
    {
        if (slotState == SlotState.NonTouchable)
            ChangeState(SlotState.Draggable);

        foreach(Slot neighbour in neighbours)
        {
            if (neighbour.SlotState == SlotState.Blocked)
                neighbour.ChangeState(SlotState.NonTouchable);
        }
    }

    public DraggableItem currentDraggableItem { get => GetComponentInChildren<DraggableItem>(); }
    public void OnDrop(PointerEventData eventData)
    {
        if (slotState == SlotState.Blocked)
        {
            return;
        }

        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (draggableItem.slot == this  || draggableItem.slot.slotState != SlotState.Draggable)
            return;

        ChangeSlot(draggableItem.slot);
        OnClick();
        currentDraggableItem.isClicked = true;
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
        if (slotState == SlotState.NonTouchable)
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

        PlayerProgressService.Progress.Coins.AddCoins(10);

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

            if (currentDraggableItem.isClicked)
            {
                CurrentItem.UseItem();
            }
            else
            {
                currentDraggableItem.isClicked = true;
            }
        }
        else
        {
            transform.parent.GetComponent<MergeGrid>().informationPanel.ActivateInfromPanel(false);
            currentDraggableItem.isClicked = false;
        }
    }

    public void ChangeState(SlotState m_slotState)
    {
        blockedImage.enabled = true;
        nontouchableImage.enabled = false;
        slotState = m_slotState;
        switch (slotState)
        {
            case SlotState.Blocked:
                break;
            case SlotState.Draggable:
                blockedImage.enabled = false;
                break;
            case SlotState.NonTouchable:
                blockedImage.enabled = false;
                nontouchableImage.enabled = true;
                break;
        }
    }

    public void DisableSelected()
    {
        //transform.parent.GetComponent<MergeGrid>().informationPanel.selectedItem.gameObject.SetActive(false);
        currentDraggableItem.isClicked = false;
    }

    public void UseItemInside()
    {
        if (!IsEmpty)
        {
            CurrentItem.UseItem();
        }
    }
}
