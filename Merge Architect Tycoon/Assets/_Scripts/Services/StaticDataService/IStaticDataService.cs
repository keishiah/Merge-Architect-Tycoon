using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Services
{
    public interface IStaticDataService
    {
        void Initialize();
        Sprite PlaceToBuildSprite { get; }
        Sprite BuildInProgressSprite { get; }
        Dictionary<string, BuildingInfo> BuildingData { get; }
        BuildingInfo GetBuildingData(string buildingName);
    }
}