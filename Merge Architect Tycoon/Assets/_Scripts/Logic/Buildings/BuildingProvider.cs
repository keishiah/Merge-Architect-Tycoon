using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BuildingProvider : IInitializableOnSceneLoaded
{
    public readonly Dictionary<string, BuildingPlace> SceneBuildingsDictionary = new();

    private BuildingCreator _buildingCreator;
    private PlayerProgress _playerProgress;
    private StaticDataService _staticDataService;
    private Cities _cities;

    [Inject]
    void Construct(BuildingCreator buildingCreator, StaticDataService staticDataService
        , PlayerProgress playerProgress, Cities cities)
    {
        _staticDataService = staticDataService;
        _playerProgress = playerProgress;
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

    public void CreateMainInTimeAsync(BuildingPlace place)
    {
        place.StartCreatingBuilding();
        _buildingCreator.CreateMainInTimeAsync(place, place.ActivityToken).Forget();
    }

    public void AddBuildingPlaceToSceneDictionary(string buildingName, BuildingPlace buildingPlace)
    {
        SceneBuildingsDictionary.Add(buildingName, buildingPlace);
    }

    public Transform GetBuildingTransform(string buildingName)
    {
        return SceneBuildingsDictionary[buildingName].GetBuildingButtonTransform();
    }

    public bool GetNextMainPart(out MainInfo mainInfo)
    {
        var mainParts = _staticDataService.MainInfoDictionary.Keys.ToList();
        foreach (var mainPart in mainParts)
        {
            if (_playerProgress.Buldings.CreatedBuildings.Contains(
                    mainPart))
                continue;

            mainInfo = _staticDataService.MainInfoDictionary[mainPart];
            return true;
        }

        mainInfo = default;
        return false;
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

        LoadMain();
    }

    private void LoadMain()
    {
        foreach (var VARIABLE in _staticDataService.MainInfoDictionary.Keys)
        {
            if (_playerProgress.Buldings.buildingsInProgress.BuildingsInProgressDict.TryGetValue(VARIABLE,
                    out var timeRest))
            {
                SceneBuildingsDictionary["Main"].buildingName = VARIABLE;
                SceneBuildingsDictionary["Main"].SetBuildingState(BuildingStateEnum.Inactive);
                if (timeRest > 0)
                    CreateMainInTimeAsync(SceneBuildingsDictionary["Main"]);
                else
                    CreateMainInTimeAsync(SceneBuildingsDictionary["Main"]);

                return;
            }

            if (_playerProgress.Buldings.CreatedBuildings.Contains(VARIABLE))
            {
                SceneBuildingsDictionary["Main"].buildingName = VARIABLE;
                SceneBuildingsDictionary["Main"].SetBuildingState(BuildingStateEnum.Inactive);

                CreateBuildingOnStart(VARIABLE);
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
        if (SceneBuildingsDictionary.Keys.Contains(buildingName))
            SceneBuildingsDictionary[buildingName].SetBuildingState(BuildingStateEnum.ShowBuilding);
        else
            SceneBuildingsDictionary["Main"].SetBuildingState(BuildingStateEnum.ShowBuilding);
    }
}