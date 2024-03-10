using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Quest : ScriptableObject
{
    [HideInInspector] public string questId;
    [HideInInspector] public QuestType questType;
    [HideInInspector] public GiveQuestCondition giveQuestCondition;

    public string questName;
    public abstract void GiveReward(Progress progress);
    public abstract List<Reward> GetRewardList();
    public abstract List<QuestItem> GetQuestItemsToCreate();
    public abstract void InitializeRewardsAndItems();
    public abstract bool IsCompleted(object value);

    protected Quest()
    {
        questId = Guid.NewGuid().ToString();
    }
}

public enum GiveQuestCondition
{
    Tutorial,
    Start,
    Building,
}

public enum QuestType
{
    Tutorial,
    Merge,
    Building,
    EarnCurrency,
    Action
}