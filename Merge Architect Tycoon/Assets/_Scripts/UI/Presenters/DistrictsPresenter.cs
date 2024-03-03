using System.Collections.Generic;
using System.Linq;
using _Scripts.Logic;
using _Scripts.Services;
using _Scripts.Services.PlayerProgressService;
using _Scripts.UI.Elements;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Scripts.UI.Presenters
{
    public class DistrictsPresenter : IInitializableOnSceneLoaded
    {
        public GameObject CitiesMapPopup;
        private int _currentDistrictId;
        public int CurrentDistrictId => _currentDistrictId;

        private Dictionary<int, int> _districtsCreatedBuildings = new();
        private readonly Dictionary<int, DistrictUi> _districts = new();

        private IPlayerProgressService _playerProgressService;
        private BuildingProvider _buildingProvider;
        private CurrencyCreator _currencyCreator;

        [Inject]
        void Construct(IPlayerProgressService playerProgressService, BuildingProvider buildingProvider,
            CurrencyCreator currencyCreator)
        {
            _playerProgressService = playerProgressService;
            _buildingProvider = buildingProvider;
            _currencyCreator = currencyCreator;
        }
        public void OnSceneLoaded()
        {
            _playerProgressService.Progress.Buldings.SubscribeToBuildingsChanges(AddCreatedBuildingToDistrictsDict);
            StartEarningCurrencyOnInitialization();        }


        public void AddDistrict(DistrictUi districtUi) => _districts.Add(districtUi.districtId, districtUi);

        public void SetCurrentDistrict(int currentDistrictId) => _currentDistrictId = currentDistrictId;

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
}