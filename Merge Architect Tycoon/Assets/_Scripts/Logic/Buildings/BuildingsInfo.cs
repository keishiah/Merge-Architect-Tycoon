using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BuildingsInfo", menuName = "StaticData/BuildingsInfo", order = 0)]
public class BuildingsInfo : ScriptableObject
{
    public List<BuildingInfo> buildings;
    public List<MainInfo> mainParts;
}

[Serializable]
public class BuildingInfo
{
    public string buildingName;
    public Sprite districtSprite;
    public Sprite buildPopupSprite;
    public int timeToCreate;
    public int coinsCountToCreate;
    public int diamondsCountToSkip;
    public List<MergeItem> itemsToCreate;
}

[Serializable]
public class MainInfo : BuildingInfo
{
}