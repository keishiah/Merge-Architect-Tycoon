using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestData
{
    public string QuestID;
    public QuestInfo QuestInfo;
    public QuestProgress[] ProgressList;

    public PlayerProgress PlayerProgress;
    public PlayerProgressService PlayerProgressService;

    public virtual void Subscribe(PlayerProgress playerProgress, PlayerProgressService playerProgressService)
    {
        PlayerProgress = playerProgress;
        PlayerProgressService = playerProgressService;
        for (int i = 0; i < QuestInfo.Objectives.Count; i++)
        {
            int index = i;//need new instance to subscribe
            QuestInfo.Objectives[i].DoSubscribe(playerProgress, ProgressList[index]);
            ProgressList[index].ProgressAction += playerProgressService.SaveQuests;
        }
    }

    public bool IsQuestComplete()
    {
        List<QuestObjective> objectives = QuestInfo.Objectives;

        if(objectives == null)
            return true;

        for (int i = 0; i < objectives.Count; i++)
        {
            if(!IsObjectiveComplete(i))
                return false;
        }
        
        return true;
    }
    public bool IsObjectiveComplete(int index)
    {
        return QuestInfo.Objectives[index].IsComplete(PlayerProgress, ProgressList[index]);
    }

    public void ClaimQuestReward()
    {
        GiveReward();
        Unsubscribe();
        PlayerProgressService.QuestComplete(this);
    }
    protected virtual void GiveReward()
    {
        foreach (Reward reward in QuestInfo.RewardList)
        {
            reward.GiveReward(PlayerProgressService);
        }
    }
    protected virtual void Unsubscribe()
    {
        for(int i = 0; i< ProgressList.Length; i++)
        {
            ProgressList[i].Subscription.Dispose();
            ProgressList[i].ProgressAction -= PlayerProgressService.SaveQuests;
        }
        ProgressList = null;
    }
}

[Serializable]
public class QuestProgress
{
    public bool IsComplete;
    public int Numeral;

    public Action ProgressAction;
    public IDisposable Subscription;
}