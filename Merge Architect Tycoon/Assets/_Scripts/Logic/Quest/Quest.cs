using System.Collections.Generic;
using UnityEngine;


public abstract class Quest : ScriptableObject
{
    public string questId;
    [HideInInspector] public GiveQuestCondition giveQuestCondition;

    public string questName;
    public abstract void GiveReward(Progress progress);
    public abstract List<Reward> GetRewardList();
    public abstract List<QuestItem> GetQuestItemsToCreate();
    public abstract void InitializeRewardsAndItems();

    public abstract bool IsCompleted(object value);
}

public enum GiveQuestCondition
{
    Tutorial,
    Building,
    Merge,
}
