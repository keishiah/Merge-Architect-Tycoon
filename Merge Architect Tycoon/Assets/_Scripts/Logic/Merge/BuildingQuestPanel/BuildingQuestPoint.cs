using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;

public class BuildingQuestPoint : MonoBehaviour
{
    [SerializeField] public Image Icon;
    [SerializeField] public TextMeshProUGUI Text;

    [SerializeField] public Image notDoneIcon;
    [SerializeField] public Image doneIcon;
    [SerializeField] public Sprite coinSprite;

    [SerializeField] public Image button;

    public int targetNumber;
    public MergeItem targetItem;
    public bool IsDone;

    private ProgressItemPopup _progressItemInfo;

    private IDisposable subscription;

    public void NewInitialization(PlayerProgress playerProgress, ProgressItemPopup _progressItemInfo, int count, MergeItem item = null)
    {
        if(subscription != null)
            subscription.Dispose();

        targetNumber = count;
        this._progressItemInfo = _progressItemInfo;

        targetItem = item;
        
        if (item == null)
        {
            Icon.sprite = coinSprite;
            subscription = playerProgress.Riches.Coins.Subscribe(SetText);
            Text.text = $"{targetNumber}";
        }
        else
        {
            Icon.sprite = item.ItemSprite;
            subscription = playerProgress.Inventory.InventoryFlag.AsObservable()
                .Subscribe(x => GetItemsCount(playerProgress));
        }
    }

    public void SetOff()
    {
        if (subscription != null)
            subscription.Dispose();

        Destroy(gameObject);
    }

    private void GetItemsCount(PlayerProgress playerProgress)
    {
        int number =
            Array.FindAll
            (
                playerProgress.Inventory.items,
                i => i.ItemID == targetItem.name && (i.SlotState == SlotState.Draggable || i.SlotState == SlotState.Unloading)
            )
            .Count();

        SetText(number);
    }

    private void SetText(int number)
    {
        if(number >= targetNumber)
        {
            Text.enabled = false;
            doneIcon.enabled = true;
            notDoneIcon.enabled = false;
            IsDone = true;
            button.enabled = false;
        }
        else
        {
            Text.enabled = true;
            doneIcon.enabled = false;
            notDoneIcon.enabled = true;
            IsDone = false;
            button.enabled = true;
            if (Icon.sprite != coinSprite)
                Text.text = $"{number} / {targetNumber}";
        }
    }

    public void OpenProgressItemInfo()
    {
        if(targetItem != null)
            _progressItemInfo.OpenProgressItemInfo(targetItem);
    }
}