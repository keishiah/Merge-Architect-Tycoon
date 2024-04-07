using System;
using System.Collections.Generic;

[Serializable]
public class QuestData
{
    public bool Done { get; private set; }

    public BaseQuestInfo QuestInfo;
    public List<QuestProgress> ProgressList;

    public Action OnActivate;
    public Action<QuestData> OnComplete;

    public virtual void Subscribe(PlayerProgress playerProgress)
    {
        for(int i = 0; i < QuestInfo.Objectives.Count; i++)
        {
            int index = i;//need new instance to subscribe
            QuestInfo.Objectives[i].DoSubscribe(playerProgress, ProgressList[index]);
        }
    }

    public bool IsComplete(PlayerProgress playerProgress)
    {
        List<QuestObjective> objectives = QuestInfo.Objectives;

        if(objectives == null)
            return true;

        for (int i = 0; i < objectives.Count; i++)
        {
            if(!objectives[i].IsComplete(playerProgress, ProgressList[i]))
                return false;
        }

        return true;
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
        Done = true;
    }
}

[Serializable]
public class QuestProgress
{
    public bool IsComplete;
    public int Value;

    public IDisposable Subscription;
}