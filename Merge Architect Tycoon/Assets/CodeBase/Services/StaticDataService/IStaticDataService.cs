using System.Collections.Generic;
using CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Services.StaticDataService
{
    public interface IStaticDataService
    {
        UniTask Initialize();
        Sprite PlaceToBuildSprite { get; }
        Sprite BuildInProgressSprite { get; }
        Dictionary<string, BuildingInfo> BuildingData { get; }
        BuildingInfo GetBuildingData(string buildingName);
    }
}