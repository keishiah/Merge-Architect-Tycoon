using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingsInProgress : ISerializationCallbackReceiver
{
    [SerializeField] private List<string> buildingName = new();
    [SerializeField] private List<int> creationRest = new();

    public Action OnAddBuildingProgress;

    public Dictionary<string, int> BuildingsInProgressDict = new();

    public bool GetBuildingCreationProgress(string name, int afkTime, out int rescaledTimeRest)
    {
        rescaledTimeRest = 0;
        if (BuildingsInProgressDict.TryGetValue(name, out int timeRest))
        {
            rescaledTimeRest = timeRest - afkTime;
        }
        else
        {
            return false;
        }

        if (rescaledTimeRest > 0)
        {
            return true;
        }


        BuildingsInProgressDict.Remove(name);
        rescaledTimeRest = 0;
        return true;
    }

    public void AddBuildingProgress(string name, int timeRest)
    {
        BuildingsInProgressDict[name] = timeRest;
        OnAddBuildingProgress?.Invoke();
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