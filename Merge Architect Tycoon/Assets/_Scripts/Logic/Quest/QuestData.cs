using System;
using System.Collections.Generic;

[Serializable]
public class QuestData
{
    public QuestInfo QuestInfo;
    public List<QuestProgress> ProgressList;

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
        for(int i = 0; i< ProgressList.Count; i++)
        {
            ProgressList[i].Subscription.Dispose();
        }
        ProgressList.Clear();
    }
}

[Serializable]
public class QuestProgress
{
    public bool IsComplete;
    public int Numeral;

    public IDisposable Subscription;
}