using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class MainPopup : MonoBehaviour
{
    public MainPlace MainPlace;
    public ResourcesPanel resourcesPanel;
    public MainRenderer mainRenderer;

    [Inject] private BuildingProvider _buildingProvider;
    [Inject] private StaticDataService _staticDataService;
    [Inject] private ItemsCatalogue _itemsCatalogue;
    [Inject] private PlayerProgress _playerProgress;
    [Inject] BackGroundButton _backgroundButton;

    private void Start()
    {
        resourcesPanel.actionButton.onClick.AddListener(StartBuild);
        _backgroundButton.button.onClick.AddListener(Closepopup);
    }

    private void StartBuild()
    {
        gameObject.SetActive(false);
        _buildingProvider.CreateMainInTimeAsync(MainPlace);
    }

    private void OnEnable()
    {
        if (_buildingProvider.GetNextMainPart(out var mainInfo))
        {
            mainRenderer.showMainPopupButton.gameObject.SetActive(false);
            MainPlace.buildingName = mainInfo.buildingName;
            SetBuildingResources(mainInfo.buildingName);
            resourcesPanel.SetButtonInteractable(
                HasEnoughResources(_staticDataService.MainInfoDictionary[mainInfo.buildingName]));
        }
    }

    private void SetBuildingResources(string buildingName)
    {
        BuildingInfo selectedBuildingData =
            _staticDataService.MainInfoDictionary[buildingName];
        var resources = selectedBuildingData.itemsToCreate;
        resourcesPanel.HideAllResources();
        foreach (var resource in selectedBuildingData.itemsToCreate.Distinct())
        {
            var currentResourceCount = _itemsCatalogue.GetItemCount(resource);
            resourcesPanel.RenderResourceElement(resource.ItemName, resource.ItemSprite,
                Mathf.Min(currentResourceCount, resources.Count((item => item.name == resource.name))),
                resources.Count((item => item.name == resource.name)));
        }

        resourcesPanel.RenderCoinsCount(
            _playerProgress.Riches.Coins.Value, selectedBuildingData.coinsCountToCreate);
    }

    private bool HasEnoughResources(BuildingInfo building)
    {
        return building.coinsCountToCreate <= _playerProgress.Riches.Coins.Value &&
               _itemsCatalogue.CheckHasItems(building.itemsToCreate);
    }

    private void Closepopup()
    {
        gameObject.SetActive(false);
        if (_buildingProvider.GetNextMainPart(out var mainInfo))
            mainRenderer.showMainPopupButton.gameObject.SetActive(true);
    }
}