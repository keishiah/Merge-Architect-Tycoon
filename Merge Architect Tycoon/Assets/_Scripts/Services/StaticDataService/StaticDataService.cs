using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

public class StaticDataService
{
    private const string BuildingsInfoPath = "BuildingInfo";
    private const string DistrictsInfoPath = "DistrictsInfo";
    private const string QuestLabel = "Quests";

    private const string TrucksInfoPath = "TrucksInfo";

    //private const string ItemsInfoPath = "ItemsInfo";
    private const string AudioDataPath = "AudioData";

    private readonly IAssetProvider _assetProvider;
    public Dictionary<string, BuildingInfo> BuildingInfoDictionary { get; private set; } = new();
    public Dictionary<string, MainInfo> MainInfoDictionary { get; private set; } = new();
    public Dictionary<int, DistrictInfo> DistrictsInfoDictionary { get; private set; } = new();
    public List<QuestInfo> Quests { get; private set; }

    public TruckInfo TruckInfo { get; private set; }

    //public List<MergeItem> ItemsInfo { get; private set; }
    public AudioData AudioData { get; private set; }

    public StaticDataService(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public async UniTask Initialize()
    {
        BuildingsInfo buildingData = await _assetProvider.Load<BuildingsInfo>(BuildingsInfoPath);
        BuildingInfoDictionary = buildingData.buildings.ToDictionary(x => x.buildingName, x => x);
        MainInfoDictionary = buildingData.mainParts.ToDictionary(x => x.buildingName, x => x);

        DistrictsInfo districtsData = await _assetProvider.Load<DistrictsInfo>(DistrictsInfoPath);
        DistrictsInfoDictionary = districtsData.districts.ToDictionary(x => x.districtId, x => x);

        IList<QuestInfo> questsData = await _assetProvider.LoadStaticDataByLabel<QuestInfo>(QuestLabel);
        Quests = questsData.ToList();

        TruckInfo = await _assetProvider.Load<TruckInfo>(TrucksInfoPath);

        AudioData = await _assetProvider.Load<AudioData>(AudioDataPath);
    }
}