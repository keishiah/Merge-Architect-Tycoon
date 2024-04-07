using System;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "BuyTruckObjective",
    menuName = "StaticData/Quests/Objectives/BuyTruckObjective")]
public class TruckQuestObjective : QuestObjective
{
    [SerializeField] private int trucksCount;

    public override void DoSubscribe(PlayerProgress playerProgress, QuestProgress questProgress)
    {
        IDisposable subscription = Observable.Start(playerProgress.Trucks.OnAddTruck)
            .Subscribe(x => questProgress.Value++);

        questProgress.Subscription = subscription;
    }

    public override string GetDescription()
    {
        return "Buy trucks";
    }

    public override string GetProgressText(QuestProgress questProgress)
    {
        return $"{questProgress.Value}/{trucksCount}";
    }

    public override bool IsComplete(PlayerProgress playerProgress, QuestProgress questProgress = null)
    {
        if (questProgress != null)
            return questProgress.Value >= trucksCount;

        return false;
    }
}
