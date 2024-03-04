using System.Collections.Generic;
using _Scripts.Logic.Buildings;
using _Scripts.Logic.CityData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Services.StaticDataService
{
    public interface IStaticDataService
    {
        UniTask Initialize();
        Sprite BuildInProgressSprite { get; }
        Dictionary<string, BuildingInfo> BuildingData { get; }
        BuildingInfo GetBuildingData(string buildingName);
        DistrictInfo GetDistrictData(int districtId);
    }
}