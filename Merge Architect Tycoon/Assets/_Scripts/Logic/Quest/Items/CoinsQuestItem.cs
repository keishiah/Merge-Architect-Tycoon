using System;

[Serializable]
public class CoinsQuestItem : QuestItem
{
    public override int GetCurrentItemCount(IPlayerProgressService progress)
    {
        return progress.Quests.CurrentQuestCoinsCount;
    }
}