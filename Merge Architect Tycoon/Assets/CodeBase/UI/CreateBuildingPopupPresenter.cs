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
        private CreateBuildingPopupScroller _createBuildingPopupScroller;
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

        public void InitializeScroller(CreateBuildingPopupScroller popupScroller)
        {
            _createBuildingPopupScroller = popupScroller;
        }

        public void InitializePopup(CreateBuildingPopup popup)
        {
            _createBuildingPopup = popup;
        }

        public void OpenScroller()
        {
            _createBuildingPopupScroller.gameObject.SetActive(true);
            _createBuildingPopupScroller.SortBuildingElements();
        }

        public void CLoseScroller()
        {
            _createBuildingPopupScroller.gameObject.SetActive(false);
        }

        public void SortBuildingElements(List<CreateBuildingUiElement> elements)
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

            foreach (var element in elements)
            {
                element.transform.SetSiblingIndex(
                    _buildings.IndexOf(_buildings.FirstOrDefault(x => x.buildingName == element.buildingName)));
            }
        }

        public void SetBuildingElements(List<CreateBuildingUiElement> elements)
        {
            var buildingInfo = _staticDataService.BuildingData.Values.ToList();

            for (int x = 0; x < buildingInfo.Count; x++)
            {
                elements[x].SetBuildingImage(buildingInfo[x].buildingSprite);
                elements[x].SetCoinsImage(buildingInfo[x].buildingSprite);
                elements[x].SetResourceImage(buildingInfo[x].buildingSprite);
                elements[x].SetCoinsPriceText(buildingInfo[x].coinsCountToCreate.ToString());
                elements[x].SetResourcesPriceText(buildingInfo[x].coinsCountToCreate.ToString());
                elements[x].SetBuildingName(buildingInfo[x].buildingName);

                elements[x].SetPresenter(this);
            }
        }

        public void SelectBuilding(CreateBuildingUiElement selectedBuilding)
        {
            TurnOfPreviousOutline(selectedBuilding);

            if (HasEnoughResources(_staticDataService.BuildingData[selectedBuilding.buildingName]))
            {
                _createBuildingPopup.OpenCreateBuildingButton();
            }
            else
            {
                _createBuildingPopup.OpenMergeButton();
            }
        }

        private bool HasEnoughResources(BuildingInfo building)
        {
            return building.coinsCountToCreate <= _playerProgressService.Progress.Coins.CurrentCoinsCount &&
                   _itemsCatalogue.CheckHasItem(building.itemToCreate);
        }

        private void TurnOfPreviousOutline(CreateBuildingUiElement selectedBuilding)
        {
            if (_currentSelectedBuildingUiElement)
                _currentSelectedBuildingUiElement.buildingImageOutline.enabled = false;
            _currentSelectedBuildingUiElement = selectedBuilding;
            selectedBuilding.buildingImageOutline.enabled = true;
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