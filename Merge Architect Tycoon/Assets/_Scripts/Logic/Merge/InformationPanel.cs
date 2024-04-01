using TMPro;
using UnityEngine;
using Zenject;

public class InformationPanel : MonoBehaviour
{
    [Inject]
    private PlayerProgressService _player;

    [Header("Texts")]
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public TMP_Text itemLvlText;
    public TMP_Text itemCostText;

    public SlotRenderer slotCurrent;

    [Space, Header("Panels")]
    public GameObject informPanel;
    public GameObject deleteButton;
    public GameObject sellButton;

    public SelectedItem selectedItem;

    public ProgressItemPopup _progressItemInfo;

    public void ConfigPanel(SlotRenderer slot)
    {
        if (slotCurrent
            && slotCurrent != slot
            && slotCurrent.currentDraggableItem() != null)
            slotCurrent.currentDraggableItem().isClicked = false;

        slotCurrent = slot;

        MergeItem mergeItem = slot.CurrentItem;
        if (mergeItem == null)
            return;

        ActivateInfromPanel(true);

        itemNameText.text = mergeItem.ItemName;
        itemDescriptionText.text = mergeItem.ItemDescrpition;
        itemLvlText.text = $"(Lvl {mergeItem.ItemLevel})";
        itemCostText.text = $"+{mergeItem.ItemCost}";
        sellButton.SetActive(false);
        deleteButton.SetActive(false);
        selectedItem.SelectSlot(slotCurrent.transform);
        if (slot.SlotState == SlotState.Draggable 
            || slot.SlotState == SlotState.Unloading)
        {
            switch (mergeItem.itemType)
            {
                case ItemType.sellable:
                    if (mergeItem.ItemCost > 0)
                        sellButton.SetActive(true);
                    else
                        deleteButton.SetActive(true);
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
        _progressItemInfo.OpenProgressItemInfo(slotCurrent.CurrentItem);
    }

    public void ActivateInfromPanel(bool state)
    {
        selectedItem.gameObject.SetActive(state);
        informPanel.SetActive(state);

        if (!(state == false && slotCurrent != null))
            return;

        DraggableItem draggableItem = slotCurrent.currentDraggableItem();
        if (draggableItem == null)
            return;

        draggableItem.isClicked = false;
    }

    public void SellItem()
    {
        if (slotCurrent.IsEmpty)
            return;

        int coins = (int)slotCurrent.CurrentItem.ItemCost;
        _player.AddCoins(coins);
        Debug.Log($"Item sell to: {coins}");

        DeleteItem();
    }

    public void DeleteItem()
    {
        if (slotCurrent.IsEmpty)
            return;
        slotCurrent.RemoveItem(isNeedSave: true);
        ActivateInfromPanel(false);
    }

    private void OnDisable()
    {
        ActivateInfromPanel(false);
    }
}