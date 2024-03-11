using System;

[Serializable]
public class MergeQuestItem : QuestItem
{
    public override int GetCurrentItemCount(Progress progress)
    {
        return progress.Quests.CurrentMergeCount;
    }
}