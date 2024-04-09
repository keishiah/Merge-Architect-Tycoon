using System;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "CompleteQuestObjective",
    menuName = "StaticData/Quests/Objectives/CompleteQuestObjective")]
public class CompleteQuestObjective : QuestObjective
{
    [SerializeField] private QuestInfo questInfo;

    public override void DoSubscribe(PlayerProgress playerProgress, QuestProgress questProgress)
    {
        IDisposable subscription = playerProgress.Quests.CompletedQuests.ObserveAdd()
            .Where(x => x.Value == questInfo.name)
            .Subscribe(x => QuestDone(questProgress));

        questProgress.Subscription = subscription;
    }

    private void QuestDone(QuestProgress questProgress)
    {
        questProgress.Numeral = 1;
        questProgress.IsComplete = true;
    }

    public override string GetDescription()
    {
        return "Complete quest " + questInfo.name;
    }

    public override string GetProgressText(QuestProgress questProgress)
    {
        return $"{questProgress.Numeral}/1";
    }

    public override bool IsComplete(PlayerProgress playerProgress, QuestProgress questProgress = null)
    {
        if (questProgress != null)
            return questProgress.IsComplete;

        return false;
    }
}