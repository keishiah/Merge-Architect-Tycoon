using System.Collections.Generic;
using UnityEngine;


public abstract class QuestBase : ScriptableObject
{
    public string questId;
    public string questText;
    public Sprite questSprite;
    
    public string requiredForActivationQuestId;
    [HideInInspector] public GiveQuestCondition giveQuestCondition;

    public abstract List<Reward> GetRewardList();
    public abstract List<QuestItem> GetQuestItemsToCreate();
    public abstract void InitializeRewardsAndItems();
    public abstract void GiveReward(IPlayerProgressService progress);
    public abstract bool IsCompleted(IPlayerProgressService progress);

    public  bool IsReadyToStart(IPlayerProgressService progress)
    {
        return requiredForActivationQuestId == "Start" ||
               progress.Quests.CompletedQuests.Contains(requiredForActivationQuestId);
    }
}

public enum GiveQuestCondition
{
    Tutorial,
    Base
}