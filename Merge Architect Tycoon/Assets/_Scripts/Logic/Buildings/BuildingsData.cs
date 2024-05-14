using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

[Serializable]
public class BuildingsData : ISerializationCallbackReceiver, IDisposable
{
    [SerializeField] private List<string> savedCreatedBuildings = new();
    public List<string> createdMainParts = new();
    private ReactiveCollection<string> _createdBuildingsReactiveCollection = new();
    public ReactiveCollection<string> CreatedBuildings => _createdBuildingsReactiveCollection;
    public BuildingsInProgress buildingsInProgress = new();

    private Subject<string> _buildingAddedSubject = new();
    public IObservable<string> BuildingAddedObservable => _buildingAddedSubject;

    public void AddCreatedBuildingToList(string buildingName)
    {
        _createdBuildingsReactiveCollection.Add(buildingName);
        _buildingAddedSubject.OnNext(buildingName);
    }

    public IDisposable SubscribeToBuildingsChanges(Action<string> onBuildingCreated)
    {
        return BuildingAddedObservable.Subscribe(onBuildingCreated);
    }

    public void OnBeforeSerialize()
    {
        savedCreatedBuildings = _createdBuildingsReactiveCollection.ToList();
    }

    public void OnAfterDeserialize()
    {
        _createdBuildingsReactiveCollection = new ReactiveCollection<string>(savedCreatedBuildings);
    }

    public int GetBuildingCount(string buildingName)
    {
        return _createdBuildingsReactiveCollection.Count(x => x == buildingName);
    }

    public void Dispose()
    {
        _createdBuildingsReactiveCollection?.Dispose();
        _buildingAddedSubject?.Dispose();
    }
}