using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Quest : ScriptableObject
{
    [HideInInspector] public string questId;
    [HideInInspector] public QuestType questType;
    
    public string questName;
    public GiveQuestCondition giveQuestCondition;
    public abstract void GiveReward();

    // public List<MergeItem> itemsToMerge;
    // public List<int> itemsCount;

    protected Quest()
    {
        questId = Guid.NewGuid().ToString();
    }
}

public enum GiveQuestCondition
{
    Start,
    Building,
}

public enum QuestType
{
    Merge,
    Building,
}