using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InformationPanel : MonoBehaviour
{
    [Inject]
    private PlayerProgressService _player;
    [Inject]
    private AudioPlayer _audio;

    [Header("Texts")]
    public Image itemImage;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public TMP_Text itemLvlText;
    public TMP_Text itemCostText;

    public TMP_Text infoText;

    public SlotRenderer slotCurrent;

    [Space, Header("Panels")]
    public GameObject informPanel;
    public GameObject deleteButton;
    public GameObject sellButton;

    public SelectedItem selectedItem;

    //public ProgressItemPopup _progressItemInfo;

    public void ConfigPanel(SlotRenderer slot)
    {
        if (slotCurrent
            && slotCurrent != slot
            && slotCurrent.RurrentDraggableItem() != null)
            slotCurrent.RurrentDraggableItem().isClicked = false;

        slotCurrent = slot;

        MergeItem mergeItem = slot.CurrentItem;
        if (mergeItem == null)
            return;

        ActivateInfromPanel(true);

        itemImage.sprite = mergeItem.ItemSprite;
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

    //public void OpenProgressInfoPanel()
    //{
    //    _progressItemInfo.OpenProgressItemInfo(slotCurrent.CurrentItem);
    //}

    public void ActivateInfromPanel(bool state)
    {
        selectedItem.gameObject.SetActive(state);
        informPanel.SetActive(state);
        infoText.gameObject.SetActive(!state);

        if (!(state == false && slotCurrent != null))
            return;

        DraggableItem draggableItem = slotCurrent.RurrentDraggableItem();
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
        _audio.PlayUiSound(UiSoundTypes.SellItem);
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