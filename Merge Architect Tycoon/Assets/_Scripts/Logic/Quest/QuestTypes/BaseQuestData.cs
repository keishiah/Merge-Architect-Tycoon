using System;

[Serializable]
public class QuestData
{
    public BaseQuestInfo QuestInfo;
    public int[] ProgressList;

    public Action OnActivate;
    public Action<QuestData> OnComplete;

    public virtual void Subscribe(PlayerProgress playerProgress)
    {
        foreach (QuestObjective objective in QuestInfo.ObjectivesList)
        {
            //objective.Subscribe(playerProgress);
        }
    }

    private void ChangeProgress(int index, int value)
    {
        ProgressList[index] = value;
        if (QuestInfo.ObjectivesList[index].IsComplete())
            IsComplete();
    }

    private void IsComplete()
    {
        foreach (QuestObjective objective in QuestInfo.ObjectivesList)
        {
            //objective.Subscribe(playerProgress);
        }
    }

    public virtual void GiveReward(PlayerProgressService progressService)
    {
        foreach (Reward reward in QuestInfo.RewardList)
        {
            reward.GiveReward(progressService);
        }
    }
}
