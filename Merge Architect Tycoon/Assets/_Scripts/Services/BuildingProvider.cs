﻿using System.Collections.Generic;
using _Scripts.Logic;
using _Scripts.Logic.Buildings;
using _Scripts.Services.PlayerProgressService;
using _Scripts.UI;
using Zenject;

namespace _Scripts.Services
{
    public class BuildingProvider : IInitializableOnSceneLoaded
    {
        public readonly Dictionary<string, BuildingPlace> SceneBuildingsDictionary = new();

        private IPlayerProgressService _playerProgressService;

        [Inject]
        void Construct(IPlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
        }

        public void OnSceneLoaded()
        {
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
}