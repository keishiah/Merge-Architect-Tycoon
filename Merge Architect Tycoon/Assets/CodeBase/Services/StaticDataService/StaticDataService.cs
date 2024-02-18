using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string BuildingsDataPath = "BuildingData";

        private Dictionary<string, BuildingInfo> _buildingData;
        public Dictionary<string, BuildingInfo> BuildingData => _buildingData;

        private Sprite _placeToBuildSprite;

        public Sprite PlaceToBuildSprite =>
            _placeToBuildSprite
                ? _placeToBuildSprite
                : throw new Exception($"PlaceToBuildSprite not initialized in StaticDataService");


        private Sprite _buildInProgressSprite;

        public Sprite BuildInProgressSprite =>
            _buildInProgressSprite
                ? _buildInProgressSprite
                : throw new Exception($"BuildInProgressSprite not initialized in StaticDataService");

        private readonly IAssetProvider _assetProvider;

        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async void Initialize()
        {
            BuildingData buildingData = await _assetProvider.Load<BuildingData>(BuildingsDataPath);
            _buildingData = buildingData.buildings.ToDictionary(x => x.buildingName, x => x);

            _placeToBuildSprite = buildingData.placeToBuildSprite;
            _buildInProgressSprite = buildingData.buildInProgressSprite;
        }

        public BuildingInfo GetBuildingData(string buildingName) =>
            _buildingData.TryGetValue(buildingName, out BuildingInfo resourceData)
                ? resourceData
                : throw new Exception($"_buildingData dictionary doesn't have {buildingName}");
    }
}