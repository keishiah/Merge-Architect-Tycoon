using UnityEngine;

[CreateAssetMenu(fileName = "CreateBuildingQuest", menuName = "StaticData/Quests/CreateFirstBuildingQuest")]
public class CreateFirstBuildingQuest : CreateBuildingQuest
{
    public CreateFirstBuildingQuest()
    {
        questType = QuestType.Building;
        giveQuestCondition = GiveQuestCondition.Tutorial;
    }
}