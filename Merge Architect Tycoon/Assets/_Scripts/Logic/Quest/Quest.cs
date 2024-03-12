using System.Collections.Generic;
using UnityEngine;


public abstract class Quest : ScriptableObject
{
    public int questId;
    public string questName;
    public int requiredForActivationQuestId;
    [HideInInspector] public GiveQuestCondition giveQuestCondition;

    public abstract List<Reward> GetRewardList();
    public abstract List<QuestItem> GetQuestItemsToCreate();
    public abstract void InitializeRewardsAndItems();
    public abstract void GiveReward(Progress progress);
    public abstract bool IsCompleted(IPlayerProgressService progress);

    public bool IsReadyToStart(IPlayerProgressService progress)
    {
        return requiredForActivationQuestId == 0 ||
               progress.Quests.CompletedQuests.Contains(requiredForActivationQuestId);
    }
}

public enum GiveQuestCondition
{
    Tutorial,
    Building,
    Merge,
}