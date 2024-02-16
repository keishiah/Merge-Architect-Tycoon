using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Services.StaticDataService
{
    public interface IStaticDataService
    {
        void Initialize();
        Sprite PlaceToBuildSprite { get; }
        Sprite BuildInProgressSprite { get; }
        BuildingInfo GetBuildingData(string buildingName);
    }
}