using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class CreateBuildingPopupPresenter
{
    private CreateBuildingPopupScroller _createBuildingPopupScroller;
    private CreateBuildingPopup _createBuildingPopup;
    private CreateBuildingUiElement _selectedBuildingElement;
    private List<CreateBuildingUiElement> _elements = new();
    private List<BuildingInfo> _buildingInfo = new();

    private ItemsCatalogue _itemsCatalogue;
    private IStaticDataService _staticDataService;
    private BuildingProvider _buildingProvider;
    private IPlayerProgressService _playerProgressService;

    private List<BuildingInfo> _sortedBuildings = new();
    private List<BuildingInfo> _readyToBuild = new();
    private List<BuildingInfo> _otherBuildings = new();

    [Inject]
    void Construct(IStaticDataService staticDataService, ItemsCatalogue itemsCatalogue,
        BuildingProvider buildingProvider, IPlayerProgressService playerProgressService,
        CreateBuildingPopup createBuildingPopup, CreateBuildingPopupScroller createBuildingPopupScroller)
    {
        _staticDataService = staticDataService;
        _itemsCatalogue = itemsCatalogue;
        _buildingProvider = buildingProvider;
        _playerProgressService = playerProgressService;
        _createBuildingPopup = createBuildingPopup;
        _createBuildingPopupScroller = createBuildingPopupScroller;
    }

    public void InitializePresenter()
    {
        _createBuildingPopup.InitializePopup();
        _createBuildingPopupScroller.InitializeScroller();
        _buildingInfo = _staticDataService.BuildingData.Values.ToList();

        SetBuildingElements();
        SortBuildingElements();

        ReactiveCollection<string> createdBuildings = _playerProgressService.Progress.Buldings.CreatedBuildings;
        foreach (string building in createdBuildings)
        {
            _createBuildingPopupScroller.RemoveBuildingElementFromPopup(building);
        }
    }

    public void InitializeBuildingElements(List<CreateBuildingUiElement> elements) => _elements = elements;

    public void OpenScroller()
    {
        SortBuildingElements();
        _createBuildingPopupScroller.OpenScroller();
        _createBuildingPopup.HideButtons();
    }

    public void RenderSortedList()
    {
        foreach (CreateBuildingUiElement element in _elements)
        {
            element.transform.SetSiblingIndex(
                _sortedBuildings.IndexOf(_sortedBuildings.FirstOrDefault(x => x.buildingName == element.buildingName)));
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
        BuildingInfo buildingData = _staticDataService.BuildingData[_selectedBuildingElement.buildingName];
        CreateBuilding(buildingData.itemsToCreate,
            buildingData.coinsCountToCreate, _selectedBuildingElement.buildingName);
    }

    private void SortBuildingElements()
    {
        _sortedBuildings.Clear();
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


        _sortedBuildings.AddRange(_readyToBuild);
        _sortedBuildings.AddRange(_otherBuildings);
    }

    private void SetBuildingElements()
    {
        for (int x = 0; x < _buildingInfo.Count; x++)
        {
            _elements[x].SetBuildingName(_buildingInfo[x].buildingName);
            _elements[x].SetBuildingImage(_buildingInfo[x].popupSprite);
            _elements[x].SetCoinsPriceText(_buildingInfo[x].coinsCountToCreate.ToString());
            _elements[x].SetResourcesImages(_buildingInfo[x].itemsToCreate);
            _elements[x].SetPresenter(this);
        }
    }

    private void CreateBuilding(List<MergeItem> items, int coinsToCreate, string buildingName)
    {
        if (_playerProgressService.Progress.Coins.SpendCoins(coinsToCreate))
        {
            _itemsCatalogue.TakeItems(items);
            _buildingProvider.CreateBuildingInTimeAsync(buildingName);
            _createBuildingPopupScroller.RemoveBuildingElementFromPopup(buildingName);
            _createBuildingPopup.HideButtons();
        }
    }

    private bool HasEnoughResources(BuildingInfo building)
    {
        return building.coinsCountToCreate <= _playerProgressService.Progress.Coins.CurrentCoinsCount &&
               _itemsCatalogue.CheckHasItems(building.itemsToCreate);
    }

    private void TurnOfPreviousOutline(CreateBuildingUiElement selectedBuilding)
    {
        if (_selectedBuildingElement)
            _selectedBuildingElement.buildingImageOutline.enabled = false;
        _selectedBuildingElement = selectedBuilding;
        selectedBuilding.buildingImageOutline.enabled = true;
    }
}