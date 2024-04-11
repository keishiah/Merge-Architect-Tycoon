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
        IDisposable subscription = playerProgress.Trucks.TruckBuyCount.AsObservable()
            .Subscribe(x => AddProgress(questProgress));
        //We compensate the method call at the subscription
        questProgress.Numeral--;

        questProgress.Subscription = subscription;
    }

    private void AddProgress(QuestProgress questProgress)
    {
        questProgress.Numeral++;
        questProgress.ProgressAction?.Invoke();
    }

    public override string GetDescription()
    {
        return "Buy trucks";
    }

    public override string GetProgressText(QuestProgress questProgress)
    {
        return $"{questProgress.Numeral}/{trucksCount}";
    }

    public override bool IsComplete(PlayerProgress playerProgress, QuestProgress questProgress = null)
    {
        if (questProgress != null)
            return questProgress.Numeral >= trucksCount;

        return false;
    }
}