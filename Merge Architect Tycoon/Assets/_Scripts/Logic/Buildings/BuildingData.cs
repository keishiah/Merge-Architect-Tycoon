using System;
using System.Collections.Generic;
using _Scripts.Logic.Merge.Items;
using UnityEngine;

namespace _Scripts.Logic.Buildings
{
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
        public Sprite buildingSprite;
        public int timeToCreate;
        public int coinsCountToCreate;
        public List<MergeItem> itemsToCreate;
    }
}