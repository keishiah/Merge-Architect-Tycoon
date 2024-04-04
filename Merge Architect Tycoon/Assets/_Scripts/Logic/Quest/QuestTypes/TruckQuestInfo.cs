using UnityEngine;

[CreateAssetMenu(fileName = "TruckQuest",
    menuName = "StaticData/Quests/TruckQuest")]
public class TruckQuestInfo : BaseQuestInfo
{
    public override bool IsCompleted(QuestData questData)
    {
        //if (questData is TruckQuestData data)
        //    return data.ProgressList[0] >= ObjectivesList[0].GoalCount;

        return false;
    }
    public override QuestData GetNewQuestData()
    {
        TruckQuestData result = new TruckQuestData();
        SetParams(result);
        return result;
    }
}
public class TruckQuestData : QuestData
{
    public override void Subscribe(PlayerProgress playerProgress)
    {
        
    }
}