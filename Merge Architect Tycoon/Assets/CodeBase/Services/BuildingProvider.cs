using CodeBase.Logic.Buildings;
using System.Collections.Generic;

namespace CodeBase.Services
{
    public class BuildingProvider
    {
        private readonly Dictionary<string, BuildingPlace> _sceneBuildingsDictionary = new();

        public void AddBuildingPlaceToSceneDictionary(string buildingName, BuildingPlace buildingPlace)
        {
            _sceneBuildingsDictionary.Add(buildingName, buildingPlace);
        }

        public void CreateBuilding(string buildingName)
        {
            if (_sceneBuildingsDictionary.TryGetValue(buildingName, out var buildingPlace))
            {
                buildingPlace.StartCreatingBuilding();
            }
        }
    }
}