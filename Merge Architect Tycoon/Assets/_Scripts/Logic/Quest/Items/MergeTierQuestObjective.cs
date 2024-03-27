public class MergeTierQuestObjective : QuestObjective
{
    public int ItemLevel;

    public override int GetCurrentItemCount()
    {
        return ItemLevel;
    }
}