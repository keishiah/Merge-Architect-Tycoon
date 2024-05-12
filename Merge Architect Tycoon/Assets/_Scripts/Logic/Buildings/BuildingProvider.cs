using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BuildingProvider : IInitializableOnSceneLoaded
{
    public readonly Dictionary<string, BuildingPlace> SceneBuildingsDictionary = new();

    private BuildingCreator _buildingCreator;
    private PlayerProgress _playerProgress;
    private Cities _cities;
    private PlayerProgressService _playerProgressService;

    [Inject]
    void Construct(BuildingCreator buildingCreator,
        PlayerProgressService playerProgressService, PlayerProgress playerProgress, Cities cities)
    {
        _playerProgress = playerProgress;
        _playerProgressService = playerProgressService;
        _buildingCreator = buildingCreator;
        _cities = cities;
    }

    public void OnSceneLoaded()
    {
        foreach (var district in _cities.Districts)
        {
            foreach (var building in district.GetComponentsInChildren<BuildingPlace>())
            {
                building.InitializeBuilding(district.districtId);
            }
        }
        
        LoadCreatedBuildings();
    }

    public void CreateBuildingInTimeAsync(string buildingName)
    {
        if (!SceneBuildingsDictionary.TryGetValue(buildingName, out var buildingPlace))
            return;

        buildingPlace.StartCreatingBuilding();
        _buildingCreator.CreateBuildingInTimeAsync(buildingPlace, buildingPlace.ActivityToken).Forget();
    }

    public void AddBuildingPlaceToSceneDictionary(string buildingName, BuildingPlace buildingPlace)
    {
        SceneBuildingsDictionary.Add(buildingName, buildingPlace);
    }

    public Transform GetBuildingTransform(string buildingName)
    {
        return SceneBuildingsDictionary[buildingName].GetBuildingButtonTransform();
    }

    private void LoadCreatedBuildings()
    {
        BuildingsData buildings = _playerProgress.Buldings;
        var buildingsInProgressDict =
            _playerProgress.Buldings.buildingsInProgress;

        var afkTime = _playerProgress.PlayerStats.GetAfkTime();
        foreach (var buildingName in SceneBuildingsDictionary.Keys)
        {
            if (buildings.CreatedBuildings.Contains(buildingName))
                CreateBuildingOnStart(buildingName);
            else if (buildingsInProgressDict.GetBuildingCreationProgress(buildingName, afkTime, out var timeRest))
            {
                if (timeRest > 0)
                {
                    ContinueBuildingCreation(buildingName, timeRest);
                }
                else
                {
                    ContinueBuildingCreation(buildingName, -1);
                }
            }
        }
    }

    private void ContinueBuildingCreation(string buildingName, int timeRest)
    {
        if (!SceneBuildingsDictionary.TryGetValue(buildingName, out var buildingPlace))
            return;

        buildingPlace.StartCreatingBuilding();
        _buildingCreator.CreateBuildingInTimeAsync(buildingPlace, buildingPlace.ActivityToken,
            timeRest).Forget();
    }


    private void CreateBuildingOnStart(string buildingName)
    {
        SceneBuildingsDictionary[buildingName].SetBuildingState(BuildingStateEnum.ShowBuilding);
    }
}