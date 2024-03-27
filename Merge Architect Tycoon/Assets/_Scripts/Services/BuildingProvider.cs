using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuildingProvider : IInitializableOnSceneLoaded
{
    public readonly Dictionary<string, BuildingPlace> SceneBuildingsDictionary = new();

    private BuildingCreator _buildingCreator;
    private IPlayerProgressService _playerProgressService;
    private District _district;

    [Inject]
    void Construct(BuildingCreator buildingCreator, IPlayerProgressService playerProgressService, District district)
    {
        _playerProgressService = playerProgressService;
        _district = district;
        _buildingCreator = buildingCreator;
    }

    public void OnSceneLoaded()
    {
        foreach (var building in _district.GetComponentsInChildren<BuildingPlace>())
        {
            building.InitializeBuilding(_district.districtId);
        }

        LoadCreatedBuildings();
    }

    public void AddBuildingPlaceToSceneDictionary(string buildingName, BuildingPlace buildingPlace)
    {
        SceneBuildingsDictionary.Add(buildingName, buildingPlace);
    }

    private void LoadCreatedBuildings()
    {
        Buldings buildings = _playerProgressService.Progress.Buldings;
        var buildingsInProgressDict =
            _playerProgressService.Progress.Buldings.buildingsInProgress.BuildingsInProgressDict;

        foreach (var buildingName in SceneBuildingsDictionary.Keys)
        {
            if (buildings.CreatedBuildings.Contains(buildingName))
                CreateBuildingOnStart(buildingName);
            else if (buildingsInProgressDict.ContainsKey(buildingName))
                ContinueBuildingCreation(buildingName, buildingsInProgressDict[buildingName]);
        }
    }

    private async void ContinueBuildingCreation(string buildingName, int timeRest)
    {
        if (!SceneBuildingsDictionary.TryGetValue(buildingName, out var buildingPlace))
            return;

        buildingPlace.StartCreatingBuilding();
        await _buildingCreator.CreateBuildingInTimeAsync(buildingPlace, buildingPlace.ActivityToken,
            timeRest);
    }


    public async void CreateBuildingInTimeAsync(string buildingName)
    {
        if (!SceneBuildingsDictionary.TryGetValue(buildingName, out var buildingPlace))
            return;

        buildingPlace.StartCreatingBuilding();
        await _buildingCreator.CreateBuildingInTimeAsync(buildingPlace, buildingPlace.ActivityToken);
    }

    private void CreateBuildingOnStart(string buildingName)
    {
        SceneBuildingsDictionary[buildingName].SetBuildingState(BuildingStateEnum.ShowBuilding);
    }
}