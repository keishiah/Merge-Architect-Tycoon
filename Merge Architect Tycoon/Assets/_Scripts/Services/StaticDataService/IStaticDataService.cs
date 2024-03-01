using System.Collections.Generic;
using _Scripts.Logic.Buildings;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Services.StaticDataService
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