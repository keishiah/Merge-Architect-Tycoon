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

    private int currentResource = 0;

    public void OnSceneLoaded()
    {
        CalculateUpdateData();

        _truckPanel.UpdateTruckButton.onClick.AddListener(UpdateTrucks);
        _truckPanel.BoostTruckButton.onClick.AddListener(BoostTrucks);
        _truckPanel.BuyTruckButton.onClick.AddListener(BuyTruck);

        _truckPanel.BoostInit(_truckInfo.BoostLimit);
        _truckPanel.RenderBoost(_data.BoostCount);

        RenderUpdateButton();

        _truckPanel.ResourcesRefresh(_data.ResourceLevel);

        //_truckZone.
    }

    private void CalculateUpdateData()
    {
        int capacity = 0, luck = 0, resources = 0;
        for(int i = 0; i < _data.UpdateLevel; i++)
        {
            switch (_truckInfo.Upgrades[i].TruskUpdate)
            {
                case TruskUpdate.Capacity:
                    capacity++;
                    break;
                case TruskUpdate.Chance:
                    luck++;
                    break;
                case TruskUpdate.NewResource:
                    resources++;
                    break;
            }
        }

        _data.CargoCapacity = _truckInfo.CargoMinCount + capacity;
        _data.LuckLevel = luck;
        _data.ResourceLevel = _truckInfo.ResourcesMinCount + resources;
    }

    public void RenderUpdateButton()
    {
        int level = _data.ResourceLevel;
        if (level >= _truckInfo.Upgrades.Length)
        {
            _truckPanel.UpdateButtonRefresh("MAX LEVEL", false);
            return;
        }

        var nextUpdate = _truckInfo.Upgrades[level];
        string text = $"+{nextUpdate.TruskUpdate} \n {nextUpdate.SoftCost}$";
        _truckPanel.UpdateButtonRefresh(text);
    }
    private void UpdateTrucks()
    {
        _progressService.UpdateTruck();
        CalculateUpdateData();
        RenderUpdateButton();
        _truckPanel.ResourcesRefresh(_data.ResourceLevel);
    }
    private void BoostTrucks()
    {
        int boostCount = _truckInfo.BoostLimit - _data.BoostCount;

        if(boostCount > 0)
            _progressService.AddBoost(boostCount);
        _truckPanel.RenderBoost(_truckInfo.BoostLimit);
    }

    public void BuyTruck()
    {
        Truck truck = new();
        truck.TruckCargo = new();
        LootBox lootBox = _truckInfo.MergeItems[currentResource].Resource;

        for(int i = 0; i < _data.CargoCapacity; i++)
        {
            MergeItem mergeItem = lootBox.GetRandomItem<MergeItem>(_data.LuckLevel);
            truck.TruckCargo.Add(mergeItem);
        }

        AddNewTruck(truck);
    }

    public void AddNewTruck(Truck truck)
    {
        _progressService.EnqueueTruck(Prepare(truck));
        _truckZone.UpdateOn();
    }

    private TruckData Prepare(Truck truckData)
    {
        string[] CargoID = new string[truckData.TruckCargo.Count];
        for(int i = 0; i < CargoID.Length; i++)
        {
            CargoID[i] = truckData.TruckCargo[i].name;
        }

        return new TruckData() { Cargo = CargoID };
    }

    public Truck Dequeue()
    {
        TruckData truckData = _progressService.DequeueTruck();
        Truck result = new Truck();
        result.TruckCargo = new();

        for (int i = 0; i < truckData.Cargo.Length; i++)
        {
            MergeItem item = Resources.Load<MergeItem>(AssetPath.Items + truckData.Cargo[i]);
            result.TruckCargo.Add(item);
        }
        result.DequeueAction += DequeueTruckItem;

        return result;
    }

    private void DequeueTruckItem()
    {
        _progressService.DequeueTruckItem();
    }
}