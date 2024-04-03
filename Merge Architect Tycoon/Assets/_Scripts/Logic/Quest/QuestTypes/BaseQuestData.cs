using System;

[Serializable]
public class QuestData
{
    public BaseQuestInfo QuestInfo;
    public int[] ProgressList;

    public Action<QuestData> OnComplete;

    public virtual void Subscribe(PlayerProgress playerProgress)
    {
    }
}
