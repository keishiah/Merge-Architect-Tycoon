using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuestInfo : ScriptableObject
{
    public string Discription;
    public Sprite Sprite;
    
    public List<Reward> RewardList;
    public List<QuestObjective> Objectives;
    public List<QuestObjective> Requires;

    public virtual bool IsReadyToStart(PlayerProgress playerProgress)
    {
        foreach(QuestObjective require in Requires)
        {
            if(!require.IsComplete(playerProgress))
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
        questData.ProgressList = new List<QuestProgress>(Objectives.Count);
        for(int i = 0; i < Objectives.Count; i++)
        {

        }
    }
}
