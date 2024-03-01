using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "BuildingData", menuName = "StaticData/BuildingData", order = 0)]
    public class BuildingData : ScriptableObject
    {
        public List<BuildingInfo> buildings;
        public Sprite placeToBuildSprite;
        public Sprite buildInProgressSprite;
    }

    [Serializable]
    public struct BuildingInfo
    {
        public string buildingName;
        public Sprite buildingSprite;
        public int timeToCreate;
        public int coinsCountToCreate;
        public MergeItem itemToCreate;
    }
}