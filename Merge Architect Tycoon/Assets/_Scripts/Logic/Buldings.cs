using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace _Scripts.Logic
{
    [Serializable]
    public class Buldings
    {
        private ReactiveCollection<string> _createdBuildings = new();

        public ReactiveCollection<string> CreatedBuildings => _createdBuildings;

        public IObservable<string> SubscribeToBuildingsChanges()
        {
            return _createdBuildings.ObserveAdd()
                .Select(evt => evt.Value);
        }

        public void AddCreatedBuildingToList(string buildingName)
        {
            _createdBuildings.Add(buildingName);
        }

        public void SubscribeToBuildingsChanges(Action<string> onBuildingCreated)
        {
            SubscribeToBuildingsChanges().Subscribe(onBuildingCreated);
        }
    }
}