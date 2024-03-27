using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

public class StaticDataService
{
    private const string BuildingsInfoPath = "BuildingInfo";
    private const string DistrictsInfoPath = "DistrictsInfo";
    private const string QuestInfoPath = "QuestsInfo";
    private const string TrucksInfoPath = "TrucksInfo";

    public Dictionary<string, BuildingInfo> BuildingInfoDictionary { get; private set; } = new();
    public Dictionary<int, DistrictInfo> DistrictsInfoDictionary { get; private set; } = new();
    public TruckInfo TruckInfo { get; private set; }

    public List<BaseQuestInfo> Quests { get; private set; }

    private readonly IAssetProvider _assetProvider;

    public StaticDataService(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public async UniTask Initialize()
    {
        BuildingsInfo buildingData = await _assetProvider.Load<BuildingsInfo>(BuildingsInfoPath);
        BuildingInfoDictionary = buildingData.buildings.ToDictionary(x => x.buildingName, x => x);

        DistrictsInfo districtsData = await _assetProvider.Load<DistrictsInfo>(DistrictsInfoPath);
        DistrictsInfoDictionary = districtsData.districts.ToDictionary(x => x.districtId, x => x);

        var questsData = await _assetProvider.LoadStaticDataByLabel<BaseQuestInfo>(QuestInfoPath);
        Quests = questsData.ToList();

        TruckInfo = await _assetProvider.Load<TruckInfo>(TrucksInfoPath);
    }
}