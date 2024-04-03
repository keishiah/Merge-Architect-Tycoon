using UnityEngine;

[CreateAssetMenu(fileName = "MergeCountQuest", menuName = "StaticData/Quests/MergeCountQuest")]
public class MergeCountQuestInfo : BaseQuestInfo
{
    public override bool IsCompleted(QuestData questData)
    {
        if (questData is MergeCountData questMergeCountData)
            return ObjectivesList[0].GoalCount >= questMergeCountData.currentMergeCount;

        return false;
    }

    public override QuestData GetNewQuestData()
    {
        return new MergeCountData();
    }
}
public class MergeCountData : QuestData
{
    public int currentMergeCount;

    public override void Subscribe(PlayerProgress playerProgress)
    {
        playerProgress.OnMergeItem += () => { currentMergeCount++; };
    }
}