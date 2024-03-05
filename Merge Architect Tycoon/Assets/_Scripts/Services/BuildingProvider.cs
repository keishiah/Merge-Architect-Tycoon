using System.Collections.Generic;
using Zenject;

public class BuildingProvider : IInitializableOnSceneLoaded
{
    public readonly Dictionary<string, BuildingPlace> SceneBuildingsDictionary = new();

    private IPlayerProgressService _playerProgressService;
    private District _district;

    [Inject]
    void Construct(IPlayerProgressService playerProgressService, District district)
    {
        _playerProgressService = playerProgressService;
        _district = district;
    }

    public void OnSceneLoaded()
    {
        foreach (var building in _district.GetComponentsInChildren<BuildingPlace>())
        {
            building.InitializeBuilding(_district.districtId);
        }

        LoadCreatedBuildings();
    }

    private void LoadCreatedBuildings()
    {
        Buldings buildings = _playerProgressService.Progress.Buldings;

        foreach (var buildingName in SceneBuildingsDictionary.Keys)
        {
            if (buildings.CreatedBuildings.Contains(buildingName))
            {
                CreateBuildingOnStart(buildingName);
            }
        }
    }

    public void AddBuildingPlaceToSceneDictionary(string buildingName, BuildingPlace buildingPlace)
    {
        SceneBuildingsDictionary.Add(buildingName, buildingPlace);
    }

    public async void CreateBuildingInTimeAsync(string buildingName)
    {
        if (SceneBuildingsDictionary.TryGetValue(buildingName, out var buildingPlace))
        {
            await buildingPlace.StartCreatingBuilding();
            _playerProgressService.Progress.AddBuilding(buildingName);
        }
    }

    private void CreateBuildingOnStart(string buildingName)
    {
        SceneBuildingsDictionary[buildingName].SetBuildingState(BuildingStateEnum.BuildingFinished);
    }
}