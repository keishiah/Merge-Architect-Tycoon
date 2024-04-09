using System;
using UniRx;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NeedItemObjective",
    menuName = "StaticData/Quests/Objectives/NeedItemObjective")]
public class NeedItemObjective : QuestObjective
{
    [SerializeField] private MergeItem neededItem;
    [SerializeField] private int count;

    public override void DoSubscribe(PlayerProgress playerProgress, QuestProgress questProgress)
    {
        IDisposable subscription = playerProgress.Inventory.InventoryFlag.AsObservable()
            .Subscribe(x => GetItemsCount(playerProgress, questProgress)) ;

        questProgress.Subscription = subscription;
    }

    private void GetItemsCount(PlayerProgress playerProgress, QuestProgress questProgress)
    {
        questProgress.Numeral = 
            Array.FindAll(playerProgress.Inventory.items, i 
            => i.ItemID == neededItem.name 
            && (i.SlotState == SlotState.Draggable || i.SlotState == SlotState.Unloading)).Count();
    }

    public override string GetDescription()
    {
        return "Combine item " + neededItem.name;
    }

    public override string GetProgressText(QuestProgress questProgress)
    {
        return $"{questProgress.Numeral}/{count}";
    }

    public override bool IsComplete(PlayerProgress playerProgress, QuestProgress questProgress = null)
    {
        if (questProgress != null)
            return questProgress.Numeral >= count;

        return false;
    }
}