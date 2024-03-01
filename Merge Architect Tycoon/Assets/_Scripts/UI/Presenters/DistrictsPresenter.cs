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
    public class DistrictsPresenter
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

        public void InitializeDistrictsPresenter()
        {
            _playerProgressService.Progress.Buldings.SubscribeToBuildingsChanges(AddCreatedBuildingToDistrictsDict);
            StartEarningCurrencyOnInitialization();
        }

        public void AddDistrict(DistrictUi districtUi) => _districts.Add(districtUi.districtId, districtUi);

        public void SetCurrentDistrict(int currentDistrictId) => _currentDistrictId = currentDistrictId;

        private void StartEarningCurrencyOnInitialization() =>
            AddCreatedBuildingToDistrictsDict(_playerProgressService.Progress.Buldings.CreatedBuildings);

        private void AddCreatedBuildingToDistrictsDict(List<string> createdBuildings)
        {
            foreach (var building in _buildingProvider.SceneBuildingsDictionary)
            {
                if (createdBuildings.Contains(building.Key))
                    if (!_districtsCreatedBuildings.Keys.Contains(building.Value.districtId))
                        AddCreatedBuildingToDistrictDict(building.Value.districtId);
            }
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
    }
}