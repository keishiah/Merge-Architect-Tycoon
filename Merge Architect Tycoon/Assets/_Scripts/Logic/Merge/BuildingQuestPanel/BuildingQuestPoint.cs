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

    [SerializeField] public Image doneIcon;
    [SerializeField] public Sprite coinSprite;

    public int targetNumber;
    public string targetItem;
    public bool IsDone;

    private IDisposable subscription;

    public void NewInitialization(PlayerProgress playerProgress, int count, MergeItem item = null)
    {
        if(subscription != null)
            subscription.Dispose();

        targetNumber = count;

        if (item == null)
        {
            Icon.sprite = coinSprite;
            subscription = playerProgress.Riches.Coins.Subscribe(SetText);
            Text.text = $"{targetNumber}";
        }
        else
        {
            Icon.sprite = item.ItemSprite;
            targetItem = item.name;
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
                i => i.ItemID == targetItem && (i.SlotState == SlotState.Draggable || i.SlotState == SlotState.Unloading)
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
            IsDone = true;
        }
        else
        {
            Text.enabled = true;
            doneIcon.enabled = false;
            IsDone = false;
            if(Icon.sprite != coinSprite)
                Text.text = $"{number} / {targetNumber}";
        }
    }
}