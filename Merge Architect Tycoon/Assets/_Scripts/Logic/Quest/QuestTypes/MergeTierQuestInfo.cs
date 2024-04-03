using UnityEngine;

[CreateAssetMenu(fileName = "MergeTierQuest",
    menuName = "StaticData/Quests/MergeTierQuest")]
public class MergeTierQuestInfo : BaseQuestInfo
{
    public override bool IsCompleted(QuestData questData)
    {
        if (questData is MergeTierQuestData data)
            return data.currentMergeTier >= ObjectivesList[0].GoalCount;

        return false;
    }
    public override QuestData GetNewQuestData()
    {
        return new MergeTierQuestData();
    }
}
public class MergeTierQuestData : QuestData
{
    public int currentMergeTier;

    public override void Subscribe(PlayerProgress playerProgress)
    {
        
    }
}