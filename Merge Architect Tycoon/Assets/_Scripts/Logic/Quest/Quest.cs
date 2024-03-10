using System;
using System.Collections.Generic;
using _Scripts.Logic.Quest;
using UnityEngine;
using UnityEngine.UI;

public abstract class Quest : ScriptableObject
{
    public string questId;
    [HideInInspector] public QuestType questType;
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
}

public enum QuestType
{
    Tutorial,
    Merge,
    Building,
    EarnCurrency,
}