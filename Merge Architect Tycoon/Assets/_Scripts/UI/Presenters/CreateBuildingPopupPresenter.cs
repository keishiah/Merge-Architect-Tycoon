using System.Collections.Generic;
using System.Linq;
using _Scripts.UI.Elements.CreateBuildingPopup;
using CodeBase.Services;
using CodeBase.StaticData;
using Zenject;

namespace _Scripts.UI.Presenters
{
    public class CreateBuildingPopupPresenter 
    {
        private string _currentBuildingName;
        private CreateBuildingUiElement _currentSelectedBuildingUiElement;

        public List<BuildingInfo> _buildings = new();
        private List<BuildingInfo> _readyToBuild = new();
        private List<BuildingInfo> _otherBuildings = new();

        private ItemsCatalogue _itemsCatalogue;
        private IStaticDataService _staticDataService;
        private BuildingProvider _buildingProvider;
        private IPlayerProgressService _playerProgressService;

        private CreateBuildingPopupScroller _createBuildingPopupScroller;
        private CreateBuildingPopup _createBuildingPopup;
        private CreateBuildingUiElement _selectedBuildingElement;
        private List<BuildingInfo> _buildingInfo = new();
        private List<CreateBuildingUiElement> _elements = new();


        [Inject]
        void Construct(IStaticDataService staticDataService, ItemsCatalogue itemsCatalogue,
            BuildingProvider buildingProvider, IPlayerProgressService playerProgressService)
        {
            _staticDataService = staticDataService;
            _itemsCatalogue = itemsCatalogue;
            _buildingProvider = buildingProvider;
            _playerProgressService = playerProgressService;
        }

        public void InitializeScroller(CreateBuildingPopupScroller popupScroller) =>
            _createBuildingPopupScroller = popupScroller;

        public void InitializePopup(CreateBuildingPopup popup) => _createBuildingPopup = popup;

        public void InitializeBuildingElements(List<CreateBuildingUiElement> elements) => _elements = elements;

        private void InitializeBuildingInfo()
        {
            _buildingInfo = _staticDataService.BuildingData.Values.ToList();
        }

        public void InitializePresenter()
        {
            InitializeBuildingInfo();
            SetBuildingElements();
            SortBuildingElements();

            var createdBuildings = _playerProgressService.Progress.Buldings.CreatedBuildings;
            foreach (string building in createdBuildings)
            {
                _createBuildingPopupScroller.RemoveBuildingElementFromPopup(building);
            }

            _createBuildingPopupScroller.SetContentWidth();
        }

        public void OpenScroller()
        {
            _createBuildingPopupScroller.gameObject.SetActive(true);
            SortBuildingElements();
            _createBuildingPopupScroller.SortBuildingElements();
        }

        public void CLoseScroller()
        {
            _createBuildingPopupScroller.gameObject.SetActive(false);
            _createBuildingPopup.ClosePanelButtons();
        }

        public void RenderSortedList()
        {
            foreach (var element in _elements)
            {
                element.transform.SetSiblingIndex(
                    _buildings.IndexOf(_buildings.FirstOrDefault(x => x.buildingName == element.buildingName)));
            }
        }

        public void SelectBuilding(CreateBuildingUiElement selectedBuilding)
        {
            TurnOfPreviousOutline(selectedBuilding);
            _selectedBuildingElement = selectedBuilding;

            if (HasEnoughResources(_staticDataService.BuildingData[selectedBuilding.buildingName]))
            {
                _createBuildingPopup.OpenCreateBuildingButton();
            }
            else
            {
                _createBuildingPopup.OpenMergeButton();
            }
        }


        public void CreateBuildingButtonClicked()
        {
            var buildingData = _staticDataService.BuildingData[_selectedBuildingElement.buildingName];
            CreateBuilding(buildingData.itemToCreate,
                buildingData.coinsCountToCreate, _selectedBuildingElement.buildingName);
        }

        private void SortBuildingElements()
        {
            _buildings.Clear();
            _readyToBuild.Clear();
            _otherBuildings.Clear();

            foreach (var building in _buildingInfo)
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
        }

        private void SetBuildingElements()
        {
            for (int x = 0; x < _buildingInfo.Count; x++)
            {
                _elements[x].SetBuildingImage(_buildingInfo[x].buildingSprite);
                _elements[x].SetResourceImage(_buildingInfo[x].itemToCreate.itemSprite);

                _elements[x].SetCoinsPriceText(_buildingInfo[x].coinsCountToCreate.ToString());
                _elements[x].SetResourcesPriceText(_buildingInfo[x].coinsCountToCreate.ToString());
                _elements[x].SetBuildingName(_buildingInfo[x].buildingName);

                _elements[x].SetPresenter(this);
            }
        }

        private void CreateBuilding(MergeItem item, int coinsToCreate, string buildingName)
        {
            if (_playerProgressService.Progress.Coins.SpendCoins(coinsToCreate))
            {
                _itemsCatalogue.TakeItems(item, 1);
                _buildingProvider.CreateBuildingInTimeAsync(buildingName);
                _createBuildingPopupScroller.RemoveBuildingElementFromPopup(buildingName);
                _createBuildingPopupScroller.SetContentWidth();
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
    }
}