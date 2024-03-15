using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingsInProgress : ISerializationCallbackReceiver
{
    [SerializeField] private List<string> buildingName = new();
    [SerializeField] private List<int> creationRest = new();

    private Dictionary<string, int> _buildingCreationProgress = new();

    public int GetBuildingCreationProgress(string tier)
    {
        return _buildingCreationProgress.ContainsKey(tier) ? _buildingCreationProgress[tier] : 0;
    }

    public void AddBuildingProgress(string name, int timeRest)
    {
        if (_buildingCreationProgress.ContainsKey(name))
        {
            _buildingCreationProgress[name] = timeRest;
        }
        else
        {
            _buildingCreationProgress.Add(name, timeRest);
        }
    }

    public void OnBeforeSerialize()
    {
        buildingName.Clear();
        creationRest.Clear();
        foreach (var kvp in _buildingCreationProgress)
        {
            buildingName.Add(kvp.Key);
            creationRest.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        _buildingCreationProgress.Clear();

        for (int i = 0; i < buildingName.Count; i++)
        {
            _buildingCreationProgress[buildingName[i]] = creationRest[i];
        }
    }
}