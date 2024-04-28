using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DistrictsPresenter : IInitializableOnSceneLoaded
{
    private int _currentDistrictId;
    public int CurrentDistrictId => _currentDistrictId;

    private readonly Dictionary<int, DistrictPopup> _districts = new();
    private readonly Dictionary<int, int> _districtsCreatedBuildings = new();
    private readonly Dictionary<int, int> _tempoDistrictsCount = new();

    private PlayerProgress _playerProgress;
    private PlayerProgressService _playerProgressService;
    private BuildingProvider _buildingProvider;
    private CurrencyCreator _currencyCreator;
    private StaticDataService _staticDataService;
    private MenuButtonsWidgetController _sceneButtons;
    private AudioPlayer _audioPlayer;
    private CitiesMapPopup _citiesMapPopup;
    private Cities _cities;

    [Inject]
    void Construct(PlayerProgress playerProgress, PlayerProgressService playerProgressService,
        StaticDataService staticDataService, BuildingProvider buildingProvider,
        CurrencyCreator currencyCreator, MenuButtonsWidgetController sceneButtons, AudioPlayer audioPlayer,
        CitiesMapPopup citiesMapPopup, Cities cities)
    {
        _playerProgress = playerProgress;
        _playerProgressService = playerProgressService;
        _buildingProvider = buildingProvider;
        _currencyCreator = currencyCreator;
        _staticDataService = staticDataService;
        _sceneButtons = sceneButtons;
        _audioPlayer = audioPlayer;
        _citiesMapPopup = citiesMapPopup;
        _cities = cities;
    }

    public void OnSceneLoaded()
    {
        InitializeDistricts();
        _playerProgress.Buldings.SubscribeToBuildingsChanges(AddCreatedBuildingToDistrictsDict);
        StartEarningCurrencyOnInitialization();
    }

    public void AddDistrict(DistrictPopup districtUi) => _districts.Add(districtUi.DistrictId, districtUi);

    public void EarnCurrency(int districtId)
    {
        var coinsForDistrict = _staticDataService.DistrictsInfoDictionary[districtId].currencyCount;
        var coinsToAdd = Mathf.RoundToInt(coinsForDistrict * .1f * _tempoDistrictsCount[districtId]);
        _audioPlayer.PlayUiSound(UiSoundTypes.AddMoney);
        _playerProgressService.AddCoins(coinsToAdd);
        TurnOnCurrencyEarningCountdown(districtId);
    }

    public void CloseMap() => _sceneButtons.CloseCurrentWidget();

    public void SetCurrentDistrict(int currentDistrictId) => _currentDistrictId = currentDistrictId;

    private void InitializeDistricts()
    {
        foreach (DistrictPopup district in _citiesMapPopup.Districts)
        {
            district.Initialize();
            if (_playerProgress.DistrictData.openedDistrictsId.Contains(district.DistrictId))
                district.ActivateDistrict(GetDistrictInfo(district.DistrictId));
        }
    }

    private DistrictInfo GetDistrictInfo(int districtId) => _staticDataService.DistrictsInfoDictionary[districtId];

    private void AddCreatedBuildingToDistrictsDict(string createdBuilding)
    {
        var createdBuildingDistrictId = _buildingProvider.SceneBuildingsDictionary[createdBuilding].districtId;

        AddCreatedBuildingToDistrictDict(createdBuildingDistrictId);
    }

    private void AddCreatedBuildingToDistrictDict(int districtId)
    {
        if (_districtsCreatedBuildings.Keys.Contains(districtId))
        {
            _districtsCreatedBuildings[districtId] += 1;
        }
        else
        {
            _districtsCreatedBuildings[districtId] = 1;
            TurnOnCurrencyEarningCountdown(districtId);
        }

        OpenNextDistrict(districtId);
    }

    private void OpenNextDistrict(int districtId)
    {
        if (_districtsCreatedBuildings[districtId] >= _cities.GetBuildingsCountInDistrict(districtId))
        {
            _playerProgressService.AddDistrict(districtId + 1);
            var nextDistrictPopup = _citiesMapPopup.GetNextDistrict(districtId);
            nextDistrictPopup.ActivateDistrict(GetDistrictInfo(districtId + 1));
        }
    }

    private void TurnOnCurrencyEarningCountdown(int districtId)
    {
        _tempoDistrictsCount[districtId] = _districtsCreatedBuildings[districtId];
        _currencyCreator.CreateCurrencyInTimeAsync(_districts[districtId]).Forget();
    }

    private void StartEarningCurrencyOnInitialization()
    {
        var createdBuildings = _playerProgress.Buldings.CreatedBuildings;

        foreach (var building in createdBuildings)
        {
            var createdBuildingDistrictId = _buildingProvider.SceneBuildingsDictionary[building].districtId;
            if (_districtsCreatedBuildings.Keys.Contains(createdBuildingDistrictId))
            {
                _districtsCreatedBuildings[createdBuildingDistrictId] += 1;
            }
            else
            {
                _districtsCreatedBuildings[createdBuildingDistrictId] = 1;
            }

            foreach (var districtId in _districtsCreatedBuildings.Keys)
            {
                TurnOnCurrencyEarningCountdown(districtId);
            }
        }
    }
}