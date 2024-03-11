using System;
using UnityEngine;

[Serializable]
public class BuildingItem : QuestItem
{
    public string buildingName;

    public BuildingItem()
    {
        itemCount = 1;
    }

    public override int GetCurrentItemCount(Progress progress)
    {
        return progress.Buldings.GetBuildingCount(buildingName);
    }
}