using System.Collections.Generic;
using System.Linq;
using CodeBase.Services;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.StaticDataService;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class CreateBuildingPopupPresenter
    {
        private string _currentBuildingName;
        private CreateBuildingUiElement _currentSelectedBuildingUiElement;

        private List<BuildingInfo> _buildings = new();
        private List<BuildingInfo> _readyToBuild = new();
        private List<BuildingInfo> _almostReady = new();
        private List<BuildingInfo> _otherBuildings = new();

        private ItemsCatalogue _itemsCatalogue;
        private IStaticDataService _staticDataService;
        private BuildingProvider _buildingProvider;
        private IPlayerProgressService _playerProgressService;
        private CreateBuildingPopup _createBuildingPopup;


        [Inject]
        void Construct(IStaticDataService staticDataService, ItemsCatalogue itemsCatalogue,
            BuildingProvider buildingProvider, IPlayerProgressService playerProgressService)
        {
            _staticDataService = staticDataService;
            _itemsCatalogue = itemsCatalogue;
            _buildingProvider = buildingProvider;
            _playerProgressService = playerProgressService;
        }

        public void SetupPopup(CreateBuildingPopup popup)
        {
            _createBuildingPopup = popup;
        }

        public void SetUpBuildingElements(List<CreateBuildingUiElement> buttons)
        {
            var buildingInfo = _staticDataService.BuildingData.Values.ToList();
            _buildings.Clear();
            _readyToBuild.Clear();
            _otherBuildings.Clear();

            foreach (var building in buildingInfo)
            {
                if (HasEnoughResources(building))
                {
                    _readyToBuild.Add(building);
                }
                else
                {
                    _otherBuildings.Add(building);
                }
            }

            _readyToBuild.Sort((a, b) => a.coinsCountToCreate.CompareTo(b.coinsCountToCreate));
            _otherBuildings.Sort((a, b) => a.coinsCountToCreate.CompareTo(b.coinsCountToCreate));


            _buildings.AddRange(_readyToBuild);
            _buildings.AddRange(_otherBuildings);


            for (int x = 0; x < _buildings.Count; x++)
            {
                buttons[x].SetBuildingImage(_buildings[x].buildingSprite);
                buttons[x].SetCoinsImage(_buildings[x].buildingSprite);
                buttons[x].SetResourceImage(_buildings[x].buildingSprite);
                buttons[x].SetCoinsPriceText(_buildings[x].coinsCountToCreate.ToString());
                buttons[x].SetResourcesPriceText(_buildings[x].coinsCountToCreate.ToString());
                buttons[x].SetBuildingName(_buildings[x].buildingName);

                buttons[x].SetCreateButtonInteractable(CheckButtonIsInteractable(_buildings[x].itemToCreate,
                    _buildings[x].coinsCountToCreate));

                buttons[x].SetPresenter(this);
            }
        }

        public void SelectBuilding(CreateBuildingUiElement selectedBuilding)
        {
            if (_currentSelectedBuildingUiElement)
                _currentSelectedBuildingUiElement.buildingImageOutline.enabled = false;
            
            _currentSelectedBuildingUiElement = selectedBuilding;
            selectedBuilding.buildingImageOutline.enabled = true;
        }

        private bool HasEnoughResources(BuildingInfo building)
        {
            return building.coinsCountToCreate <= _playerProgressService.Progress.Coins.CurrentCoinsCount &&
                   _itemsCatalogue.CheckHasItem(building.itemToCreate);
        }


        private bool CheckButtonIsInteractable(MergeItem itemToCreate, int coinsToCreate)
        {
            return _itemsCatalogue.CheckHasItem(
                itemToCreate) && _playerProgressService.Progress.Coins.CurrentCoinsCount >= coinsToCreate;
        }

        // private void SelectBuilding(MergeItem item, int coinsToCreate, string buildingName)
        // {
        //     if (_playerProgressService.Progress.Coins.SpendCoins(coinsToCreate))
        //     {
        //         _itemsCatalogue.TakeItems(item, 1);
        //         _buildingProvider.CreateBuilding(buildingName);
        //
        //         _createBuildingPopup.gameObject.SetActive(false);
        //     }
        // }
    }
}