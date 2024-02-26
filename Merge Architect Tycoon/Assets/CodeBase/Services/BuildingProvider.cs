﻿using CodeBase.Logic.Buildings;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Services.PlayerProgressService;
using CodeBase.UI;
using Zenject;

namespace CodeBase.Services
{
    public class BuildingProvider
    {
        private readonly Dictionary<string, BuildingPlace> _sceneBuildingsDictionary = new();

        private IPlayerProgressService _playerProgressService;

        [Inject]
        void Construct(IPlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
        }

        public void AddBuildingPlaceToSceneDictionary(string buildingName, BuildingPlace buildingPlace)
        {
            Buldings buildings = _playerProgressService.Progress.Buldings;

            _sceneBuildingsDictionary.Add(buildingName, buildingPlace);
            if (buildings.CreatedBuildings.Contains(buildingName))
                CreateBuildingOnStart(buildingName);
        }

        public async void CreateBuildingInTimeAsync(string buildingName)
        {
            Buldings buildings = _playerProgressService.Progress.Buldings;
            if (_sceneBuildingsDictionary.TryGetValue(buildingName, out var buildingPlace))
            {
                await buildingPlace.StartCreatingBuilding();
                buildings.AddCreatedBuildingToList(buildingName);
            }
        }

        private void CreateBuildingOnStart(string buildingName)
        {
            _sceneBuildingsDictionary[buildingName].SetBuildingState(BuildingStateEnum.BuildingFinished);
        }
    }
}