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
    public abstract void GiveReward();
    public abstract List<Reward> GetRewardList();
    public abstract List<QuestItem> GetQuestItemsToCreate();
    public abstract bool IsCompleted<T>(T value);

// public List<MergeItem> itemsToMerge;
// public List<int> itemsCount;

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
    Merge,
    Building,
}