using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "StaticData/BuildingData", order = 0)]
public class BuildingData : ScriptableObject
{
    public List<BuildingInfo> buildings;
    public Sprite buildInProgressSprite;
}

[Serializable]
public struct BuildingInfo
{
    public string buildingName;
    public Sprite districtSprite;
    public Sprite popupSprite;
    public int timeToCreate;
    public int coinsCountToCreate;
    public List<MergeItem> itemsToCreate;
}