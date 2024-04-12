using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest",
    menuName = "StaticData/Quests/Quest")]
public class QuestInfo : ScriptableObject
{
    public string Discription;
    public Sprite Sprite;
    public List<QuestObjective> Requires;

    [Header("BODY")]
    public List<QuestObjective> Objectives;
    public List<Reward> RewardList;

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

    public void SetParams(QuestData questData)
    {
        questData.QuestID = name;
        questData.QuestInfo = this;
        if(questData.ProgressList == null)
        {
            questData.ProgressList = new QuestProgress[Objectives.Count];
            for(int i = 0; i < Objectives.Count; i++)
            {
                questData.ProgressList[i] = new QuestProgress();
            }
        }
        
    }
}
