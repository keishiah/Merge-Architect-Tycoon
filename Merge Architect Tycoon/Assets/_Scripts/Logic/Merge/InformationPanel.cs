using TMPro;
using UnityEngine;

public class InformationPanel : MonoBehaviour
{
    [Header("Texts")]
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public TMP_Text itemLvlText;
    public TMP_Text itemCostText;

    public Slot slotCurrent;

    [Space, Header("Panels")]
    public GameObject informPanel;
    public GameObject deleteButton;
    public GameObject sellButton;


    public SelectedItem selectedItem;

    public ProgressItemInfo progressItemInfo;

    public void ConfigPanel(Slot slot)
    {
        if (slotCurrent)
        {
            if (slotCurrent != slot)
            {
                if (slotCurrent.currentDraggableItem)
                    slotCurrent.currentDraggableItem.isClicked = false;
            }
        }

        slotCurrent = slot;

        MergeItem mergeItem = slot.CurrentItem;
        if (mergeItem == null)
            return;

        ActivateInfromPanel(true);

        itemNameText.text = mergeItem.itemName;
        itemDescriptionText.text = mergeItem.itemDescrpition;
        itemLvlText.text = $"(Lvl {mergeItem.itemLevel})";
        itemCostText.text = $"+{mergeItem.itemCost}";
        sellButton.SetActive(false);
        deleteButton.SetActive(false);
        //selectedItem.SelectSlot(slotCurrent);
        if (slot.SlotState == SlotState.Draggable)
        {
            switch (mergeItem.itemType)
            {
                case ItemType.sellable:
                    if (mergeItem.itemCost > 0)
                    {
                        sellButton.SetActive(true);
                    }
                    else
                    {
                        deleteButton.SetActive(true);
                    }
                    break;
                case ItemType.spawner:

                    break;
                case ItemType.unsellable:
                    break;
            }
        }

    }

    public void OpenProgressInfoPanel()
    {
        //progressItemInfo.OpenProgressItemInfo(slotCurrent.CurrentItem);
    }

    public void ActivateInfromPanel(bool state)
    {
        //selectedItem.gameObject.SetActive(state);
        informPanel.SetActive(state);

        if (state == false)
        {
            if (slotCurrent)
                slotCurrent.currentDraggableItem.isClicked = false;
        }
    }

    public void SellItem()
    {
        if (slotCurrent.IsEmpty)
            return;
        Debug.Log($"Item sell to: {slotCurrent.CurrentItem.itemCost}");

        DeleteItem();
    }

    public void DeleteItem()
    {
        if (slotCurrent.IsEmpty)
            return;
        slotCurrent.RemoveItem();
        ActivateInfromPanel(false);
    }

    private void OnDisable()
    {
        ActivateInfromPanel(false);
    }
}
