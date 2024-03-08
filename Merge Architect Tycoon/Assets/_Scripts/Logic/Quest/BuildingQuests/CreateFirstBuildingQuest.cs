using UnityEngine;

[CreateAssetMenu(fileName = "CreateBuildingQuest", menuName = "StaticData/Quests/CreateFirstBuildingQuest")]
public class CreateFirstBuildingQuest : CreateBuildingQuest
{
    public override bool IsCompleted(string buildingName)
    {
        return buildingName == "House";
    }
}