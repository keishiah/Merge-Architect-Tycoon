using System;

[Serializable]
public class MergeQuestItem : QuestItem
{
    public override int GetCurrentItemCount(IPlayerProgressService progress)
    {
        return progress.Quests.CurrentMergeCount;
    }
}