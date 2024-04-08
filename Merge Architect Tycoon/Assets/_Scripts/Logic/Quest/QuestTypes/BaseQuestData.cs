using System;
using System.Collections.Generic;
using UniRx;

[Serializable]
public class QuestData
{
    public QuestBaseInfo QuestInfo;
    public List<QuestProgress> ProgressList;

    public PlayerProgress playerProgress;

    public virtual void Subscribe(PlayerProgress playerProgress)
    {
        this.playerProgress = playerProgress;
        for(int i = 0; i < QuestInfo.Objectives.Count; i++)
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
        return QuestInfo.Objectives[index].IsComplete(playerProgress, ProgressList[index]);
    }
    public virtual void GiveReward(PlayerProgressService progressService)
    {
        foreach (Reward reward in QuestInfo.RewardList)
        {
            reward.GiveReward(progressService);
        }

        Unsubscribe();
    }

    private void Unsubscribe()
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