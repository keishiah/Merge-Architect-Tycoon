using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

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
}