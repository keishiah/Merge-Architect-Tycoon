using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Logic
{
    [Serializable]
    public class Buldings : ISerializationCallbackReceiver
    {
        [SerializeField] private List<string> savedCreatedBuildings = new List<string>();

        private ReactiveCollection<string> _createdBuildingsReactiveCollection = new ReactiveCollection<string>();
        public ReactiveCollection<string> CreatedBuildings => _createdBuildingsReactiveCollection;

        private Subject<string> _buildingAddedSubject = new Subject<string>();
        public IObservable<string> BuildingAddedObservable => _buildingAddedSubject;

        public void AddCreatedBuildingToList(string buildingName)
        {
            _createdBuildingsReactiveCollection.Add(buildingName);
            _buildingAddedSubject.OnNext(buildingName);
        }

        public void SubscribeToBuildingsChanges(Action<string> onBuildingCreated)
        {
            BuildingAddedObservable.Subscribe(onBuildingCreated);
        }

        public void OnBeforeSerialize()
        {
            savedCreatedBuildings = _createdBuildingsReactiveCollection.ToList();
        }

        public void OnAfterDeserialize()
        {
            _createdBuildingsReactiveCollection = new ReactiveCollection<string>(savedCreatedBuildings);
        }
        // [SerializeField] private List<string> savedCreatedBuildings = new();
        //
        // private ReactiveCollection<string> _createdBuildingsReactiveCollection = new();
        // public ReactiveCollection<string> CreatedBuildings => _createdBuildingsReactiveCollection;
        //
        // private IObservable<string> BuildingCreatedObservable()
        // {
        //     return _createdBuildingsReactiveCollection.ObserveAdd()
        //         .Select(evt => evt.Value);
        // }
        //
        // public void AddCreatedBuildingToList(string buildingName)
        // {
        //     _createdBuildingsReactiveCollection.Add(buildingName);
        // }
        //
        // public void SubscribeToBuildingsChanges(Action<string> onBuildingCreated)
        // {
        //     BuildingCreatedObservable().Subscribe(onBuildingCreated);
        // }
        //
        // public void OnBeforeSerialize()
        // {
        //     savedCreatedBuildings.Clear();
        //     foreach (var item in _createdBuildingsReactiveCollection)
        //     {
        //         savedCreatedBuildings.Add(item);
        //     }
        //
        //     BuildingCreatedObservable().Subscribe(savedCreatedBuildings.Add);
        // }
        //
        // public void OnAfterDeserialize()
        // {
        //     _createdBuildingsReactiveCollection.Clear();
        //     foreach (var item in savedCreatedBuildings)
        //     {
        //         _createdBuildingsReactiveCollection.Add(item);
        //     }
        // }
    }
}