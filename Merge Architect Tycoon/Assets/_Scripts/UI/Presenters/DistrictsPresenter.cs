using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DistrictsPresenter : IInitializableOnSceneLoaded
{
    public GameObject CitiesMapPopup;
    private int _currentDistrictId;
    public int CurrentDistrictId => _currentDistrictId;

    private Dictionary<int, int> _districtsCreatedBuildings = new();
    private readonly Dictionary<int, DistrictUi> _districts = new();
    private readonly Dictionary<int, int> _tempoDistrictsCount = new();

    private IPlayerProgressService _playerProgressService;
    private BuildingProvider _buildingProvider;
    private CurrencyCreator _currencyCreator;
    private IStaticDataService _staticDataService;

    [Inject]
    void Construct(IPlayerProgressService playerProgressService, IStaticDataService staticDataService,
        BuildingProvider buildingProvider,
        CurrencyCreator currencyCreator)
    {
        _playerProgressService = playerProgressService;
        _buildingProvider = buildingProvider;
        _currencyCreator = currencyCreator;
        _staticDataService = staticDataService;
    }

    public void OnSceneLoaded()
    {
        _playerProgressService.Progress.Buldings.SubscribeToBuildingsChanges(AddCreatedBuildingToDistrictsDict);
        StartEarningCurrencyOnInitialization();
    }


    public void AddDistrict(DistrictUi districtUi) => _districts.Add(districtUi.districtId, districtUi);

    public void SetCurrentDistrict(int currentDistrictId) => _currentDistrictId = currentDistrictId;

    public void EarnCurrency(int districtId)
    {
        var coinsForDistrict = _staticDataService.GetDistrictData(districtId).currencyCount;
        var coinsToAdd = Mathf.RoundToInt(coinsForDistrict * .1f * _tempoDistrictsCount[districtId]);
        _playerProgressService.Progress.AddCoins(coinsToAdd);

        TurnOnCurrencyEarningCountdown(districtId);
    }

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
    }

    private void TurnOnCurrencyEarningCountdown(int districtId)
    {
        _tempoDistrictsCount[districtId] = _districtsCreatedBuildings[districtId];
        _currencyCreator.CreateCurrencyInTimeAsync(_districts[districtId]).Forget();
    }

    private void StartEarningCurrencyOnInitialization()
    {
        var createdBuildings = _playerProgressService.Progress.Buldings.CreatedBuildings;

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