using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TruckProvider : IInitializableOnSceneLoaded
{
    [Inject] private TruckPanel _truckPanel;
    [Inject] private TruckZoneRenderer _truckZone;
    [Inject] private PlayerProgressService _progressService;
    [Inject] private PlayerProgress _playerProgress;
    [Inject] private StaticDataService _staticDataService;

    private TruckInfo _truckInfo => _staticDataService.TruckInfo;
    private TrucksData _data => _playerProgress.Trucks;
    private Queue<Truck> _trucks = new();

    public void OnSceneLoaded()
    {
        _truckPanel.BoostInit(_truckInfo.BoostLimit);
        _truckPanel.ResourcesRefresh(_data.Level.Value);
        RenderUpdateButton();
    }

    public void RenderUpdateButton()
    {
        int level = _data.Level.Value;
        if (level >= _truckInfo.Upgrades.Length)
        {
            _truckPanel.UpdateButtonRefresh("MAX LEVEL", false);
            return;
        }

        var nextUpdate = _truckInfo.Upgrades[level];
        string text = $"+{nextUpdate.TruskUpdate} \n {nextUpdate.SoftCost}$";
        _truckPanel.UpdateButtonRefresh(text);
    }

    public void AddNewTruck(Truck truck)
    {
        _progressService.EnqueueTruck(Prepare(truck));
        _trucks.Enqueue(truck);
        _truckZone.UpdateOn();
    }

    private TruckData Prepare(Truck truckData)
    {
        string[] CargoID = new string[truckData.TruckCargo.Count];
        for(int i = 0; i < CargoID.Length; i++)
        {
            CargoID[i] = truckData.TruckCargo[i].ItemName;
        }

        return new TruckData() { Cargo = CargoID };
    }

    public Truck Dequeue()
    {
        TruckData truckData = _progressService.DequeueTruck();
        Truck result = new Truck();

        for(int i = 0; i < truckData.Cargo.Length; i++)
        {
            result.TruckCargo[i] = Resources.Load<MergeItem>(AssetPath.Items + truckData.Cargo[i]);
        }
        result.DequeueAction += DequeueTruckItem;

        return result;
    }

    private void DequeueTruckItem()
    {
        _progressService.DequeueTruckItem();
    }
}