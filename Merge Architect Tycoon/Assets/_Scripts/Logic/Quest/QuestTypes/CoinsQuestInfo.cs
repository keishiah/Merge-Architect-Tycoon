using UnityEngine;

[CreateAssetMenu(fileName = "CoinsQuest", menuName = "StaticData/Quests/CoinsQuest")]
public class CoinsQuestInfo : BaseQuestInfo
{
    public override bool IsCompleted(QuestData questData)
    {
        if(questData is CoinsQuestData data)
        {
            bool isComplete = data.currentCoinsCount >= ObjectivesList[0].GoalCount;
            if (isComplete)
            {
                data.OnComplete?.Invoke(questData);
                return true;
            }
        }

        return false;
    }
    public override QuestData GetNewQuestData()
    {
        return new CoinsQuestData();
    }
}
public class CoinsQuestData : QuestData
{
    public int currentCoinsCount;

    public override void Subscribe(PlayerProgress playerProgress)
    {
       
    }
}