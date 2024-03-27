using System;

[Serializable]
public class BuildingObjective : QuestObjective
{
    public string buildingName;

    public BuildingObjective()
    {
        GoalCount = 1;
    }
}