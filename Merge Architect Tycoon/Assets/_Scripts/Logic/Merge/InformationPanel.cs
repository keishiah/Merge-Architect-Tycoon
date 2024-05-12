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

    [Header("Sprites")]
    public Sprite DefaultSprite;
    public Sprite OnSelectionSprite;

    [Header("Texts")]
    public Image itemImage;
    public Image backgroundImage;
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
        selectedItem.SelectSlot(slotCurrent.transform);
    }

    public void ActivateInfromPanel(bool state)
    {
        backgroundImage.sprite = state ? OnSelectionSprite : DefaultSprite;

        selectedItem.gameObject.SetActive(state);
        sellButton.SetActive(state);
        itemNameText.gameObject.SetActive(state);
        //infoText.gameObject.SetActive(!state);

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
        _audio.PlayUiSound(UiSoundTypes.AddMoney);
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