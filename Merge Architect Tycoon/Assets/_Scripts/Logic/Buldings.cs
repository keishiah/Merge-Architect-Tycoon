using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace _Scripts.Logic
{
    [Serializable]
    public class Buldings
    {
        private ReactiveProperty<List<string>> _createdBuildings = new(new List<string>());

        public List<string> CreatedBuildings => _createdBuildings.Value;

        public void AddCreatedBuildingToList(string buildingName)
        {
            _createdBuildings.Value.Add(buildingName);
        }

        public void SubscribeToBuildingsChanges(Action<List<string>> onBuildingCreated)
        {
            _createdBuildings.Subscribe(onBuildingCreated);
        }
    }
}