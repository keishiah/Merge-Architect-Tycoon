using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingsInProgress : ISerializationCallbackReceiver
{
    [SerializeField] private List<string> buildingName = new();
    [SerializeField] private List<int> creationRest = new();

    public Dictionary<string, int> BuildingsInProgressDict = new();

    public int GetBuildingCreationProgress(string tier)
    {
        return BuildingsInProgressDict.ContainsKey(tier) ? BuildingsInProgressDict[tier] : 0;
    }

    public void AddBuildingProgress(string name, int timeRest)
    {
        if (BuildingsInProgressDict.ContainsKey(name))
        {
            BuildingsInProgressDict[name] = timeRest;
        }
        else
        {
            BuildingsInProgressDict.Add(name, timeRest);
        }
    }

    public void RemoveBuildingProgress(string name) => BuildingsInProgressDict.Remove(name);

    public void OnBeforeSerialize()
    {
        buildingName.Clear();
        creationRest.Clear();
        foreach (var kvp in BuildingsInProgressDict)
        {
            buildingName.Add(kvp.Key);
            creationRest.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        BuildingsInProgressDict.Clear();

        for (int i = 0; i < buildingName.Count; i++)
        {
            BuildingsInProgressDict[buildingName[i]] = creationRest[i];
        }
    }
}