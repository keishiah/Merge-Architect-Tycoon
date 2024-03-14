using System;

[Serializable]
public class MergeTierQuestItem : QuestItem
{
    public int ItemLevel;

    public override int GetCurrentItemCount(IPlayerProgressService progress)
    {
        return progress.Quests.mergeTierQuests.GetCurrentItemCount(ItemLevel);
    }
}