using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Services.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private const string BuildingsDataPath = "BuildingData";
    private const string DistrictsDataPath = "DistrictsData";
    private const string AudioDataPath = "AudioData";

    private const string QuestDataPath = "Quests";

    private Dictionary<string, BuildingInfo> _buildingData;
    public Dictionary<string, BuildingInfo> BuildingData => _buildingData;

    private Dictionary<int, DistrictInfo> _districtsData;

    private Sprite _buildInProgressSprite;

    public Sprite BuildInProgressSprite =>
        _buildInProgressSprite
            ? _buildInProgressSprite
            : throw new Exception($"BuildInProgressSprite not initialized in StaticDataService");

    public List<QuestBase> Quests { get; private set; }

    private readonly IAssetProvider _assetProvider;

    public AudioData AudioData { get; private set; }

    public StaticDataService(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public async UniTask Initialize()
    {
        BuildingData buildingData = await _assetProvider.Load<BuildingData>(BuildingsDataPath);
        _buildingData = buildingData.buildings.ToDictionary(x => x.buildingName, x => x);

        DistrictsData districtsData = await _assetProvider.Load<DistrictsData>(DistrictsDataPath);
        _districtsData = districtsData.districts.ToDictionary(x => x.districtId, x => x);

        var questsData = await _assetProvider.LoadStaticDataByLabel<QuestBase>(QuestDataPath);
        Quests = questsData.ToList();

        AudioData = await _assetProvider.Load<AudioData>(AudioDataPath);
        _buildInProgressSprite = buildingData.buildInProgressSprite;
    }

    public BuildingInfo GetBuildingData(string buildingName) =>
        _buildingData.TryGetValue(buildingName, out BuildingInfo resourceData)
            ? resourceData
            : throw new Exception($"_buildingData dictionary doesn't have {buildingName}");

    public DistrictInfo GetDistrictData(int districtId) =>
        _districtsData.TryGetValue(districtId, out DistrictInfo districtsData)
            ? districtsData
            : throw new Exception($"_districtsData dictionary doesn't have {districtId}");
}