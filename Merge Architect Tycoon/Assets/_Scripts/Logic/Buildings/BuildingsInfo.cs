using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsInfo", menuName = "StaticData/BuildingsInfo", order = 0)]
public class BuildingsInfo : ScriptableObject
{
    public List<BuildingInfo> buildings;
}

[Serializable]
public struct BuildingInfo
{
    public string buildingName;
    public Sprite districtSprite;
    public Sprite buildPopupSprite;
    public int timeToCreate;
    public int coinsCountToCreate;
    public int diamondsCountToSkip;
    public List<MergeItem> itemsToCreate;
}