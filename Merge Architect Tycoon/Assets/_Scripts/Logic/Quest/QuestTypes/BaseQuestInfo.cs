using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuestInfo : ScriptableObject
{
    public string ID;
    public string Text;
    public Sprite Sprite;
    
    public string RequiredForActivationQuestId;
    [HideInInspector] public GiveQuestCondition GiveQuestCondition;

    public abstract List<Reward> GetRewardList();
    public abstract List<QuestObjective> GetQuestObjectives();
    public abstract void InitializeRewardsAndItems();
    public abstract void GiveReward(PlayerProgressService progressService);
    public abstract bool IsCompleted(QuestData questData);

    public  bool IsReadyToStart(PlayerProgress progress)
    {
        return RequiredForActivationQuestId == "Start" ||
               progress.Quests.CompletedQuests.Contains(RequiredForActivationQuestId);
    }

    public abstract QuestData GetNewQuestData();
}

public enum GiveQuestCondition
{
    Tutorial,
    Base
}