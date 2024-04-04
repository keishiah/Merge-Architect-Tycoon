using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuestInfo : ScriptableObject
{
    public string Discription;
    public Sprite Sprite;
    
    public List<Reward> RewardList;
    public List<QuestObjective> ObjectivesList;
    public List<string> RequiredCompletedQuests;

    public virtual void GiveReward(PlayerProgressService progressService)
    {
        foreach (Reward reward in RewardList)
        {
            reward.GiveReward(progressService);
        }
    }
    public virtual bool IsCompleted(QuestData questData)
    {
        for(int i = 0; i < ObjectivesList.Count; i++)
        {
            if(!ObjectivesList[i].IsComplete())
                return false;
        }

        return true;
    }
    public virtual bool IsReadyToStart(PlayerProgress progress)
    {
        foreach(string questID in RequiredCompletedQuests)
        {
            if(!progress.Quests.CompletedQuests.Contains(questID))
                return false;
        }

        return true;
    }
    public virtual QuestData GetNewQuestData()
    {
        QuestData result = new QuestData();
        SetParams(result);
        return result;
    }

    protected void SetParams(QuestData questData)
    {
        questData.QuestInfo = this;
        questData.ProgressList = new int[ObjectivesList.Count];
    }
}