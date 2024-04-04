using UnityEngine;

[CreateAssetMenu(fileName = "CreateBuildingObjective",
    menuName = "StaticData/Quests/Objectives/CreateBuildingObjective")]
public class BuildingObjective : QuestObjective
{
    public string buildingName;

    public override string GetProgress()
    {
        return string.Empty;
    }

    public override bool IsComplete()
    {
        return false;
            //progress.Buldings.CreatedBuildings.Contains(buildingName);
    }
}