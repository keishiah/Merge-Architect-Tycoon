using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

public class CreateBuildingPopupPresenter
{
    private CreateBuildingPopupScroller _createBuildingPopupScroller;
    private CreateBuildingPopup _createBuildingPopup;
    private CreateBuildingUiElement _selectedBuildingElement;
    private List<CreateBuildingUiElement> _elements = new();
    private List<BuildingInfo> _buildingInfo = new();

    private ItemsCatalogue _itemsCatalogue;
    private StaticDataService _staticDataService;
    private BuildingProvider _buildingProvider;
    private PlayerProgress _playerProgress;
    private PlayerProgressService _playerProgressService;

    private List<BuildingInfo> _sortedBuildings = new();
    private List<BuildingInfo> _readyToBuild = new();
    private List<BuildingInfo> _otherBuildings = new();

    [Inject]
    void Construct(StaticDataService staticDataService, ItemsCatalogue itemsCatalogue,
        BuildingProvider buildingProvider, 
        PlayerProgress playerProgress, PlayerProgressService playerProgressService,
        CreateBuildingPopup createBuildingPopup, CreateBuildingPopupScroller createBuildingPopupScroller)
    {
        _staticDataService = staticDataService;
        _itemsCatalogue = itemsCatalogue;
        _buildingProvider = buildingProvider;
        _playerProgress = playerProgress;
        _playerProgressService = playerProgressService;
        _createBuildingPopup = createBuildingPopup;
        _createBuildingPopupScroller = createBuildingPopupScroller;
    }

    public void InitializePresenter()
    {
        _createBuildingPopup.InitializePopup();
        _createBuildingPopupScroller.InitializeScroller();
        _buildingInfo = _staticDataService.BuildingInfoDictionary.Values.ToList();

        SetBuildingElements();
        SortBuildingElements();

        ReactiveCollection<string> createdBuildings = _playerProgress.Buldings.CreatedBuildings;
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
        if (HasEnoughResources(_staticDataService.BuildingInfoDictionary[selectedBuilding.buildingName]))
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
        BuildingInfo buildingData = _staticDataService.BuildingInfoDictionary[_selectedBuildingElement.buildingName];
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
            _elements[x].SetBuildingImage(_buildingInfo[x].buildPopupSprite);
            _elements[x].SetCoinsPriceText(_buildingInfo[x].coinsCountToCreate.ToString());
            _elements[x].SetResourcesImages(_buildingInfo[x].itemsToCreate);
            _elements[x].SetPresenter(this);
        }
    }

    private void CreateBuilding(List<MergeItem> items, int coinsToCreate, string buildingName)
    {
        if (_playerProgressService.SpendCoins(coinsToCreate))
        {
            _itemsCatalogue.TakeItems(items);
            _buildingProvider.CreateBuildingInTimeAsync(buildingName);
            _createBuildingPopupScroller.RemoveBuildingElementFromPopup(buildingName);
            _createBuildingPopup.HideButtons();
        }
    }

    private bool HasEnoughResources(BuildingInfo building)
    {
        return building.coinsCountToCreate <= _playerProgress.Riches.Coins.Value &&
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