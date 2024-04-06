using UnityEngine;

[CreateAssetMenu(fileName = "CoinsQuest", menuName = "StaticData/Quests/CoinsQuest")]
public class CoinsQuestInfo : BaseQuestInfo
{
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