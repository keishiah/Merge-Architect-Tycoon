using System;

[Serializable]
public class TruckQuestItem : QuestItem
{
    public override int GetCurrentItemCount(IPlayerProgressService progress)
    {
        return progress.Quests.CurrentTruckCount;
    }
}