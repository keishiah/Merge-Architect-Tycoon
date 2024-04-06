using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuestInfo : ScriptableObject
{
    public string Discription;
    public Sprite Sprite;
    
    public List<Reward> RewardList;
    public List<QuestObjective> ObjectivesList;
    public List<QuestRequires> Requires;

    public virtual bool IsReadyToStart(PlayerProgress progress)
    {
        foreach(QuestRequires require in Requires)
        {
            if(!require.IsComplete())
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
