using System;

[Serializable]
public abstract class QuestData
{
    public BaseQuestInfo QuestInfo;
    public Action<QuestData> OnComplete;
    public abstract void Subscribe(PlayerProgress playerProgress);
}
