using UnityEngine;
using UniRx;
using Zenject;
using System;

public class TruckProvider : IInitializableOnSceneLoaded
{
    [Inject] private TruckPanel _truckPanel;
    [Inject] private TruckZoneRenderer _truckZone;
    [Inject] private PlayerProgressService _progressService;
    [Inject] private PlayerProgress _playerProgress;
    [Inject] private StaticDataService _staticDataService;

    private TruckInfo _truckInfo => _staticDataService.TruckInfo;
    private TrucksData _data => _playerProgress.Trucks;

    private int resourceCost => _truckInfo.MergeItems[_data.CurrentResource].SoftCost;
    private int boostCost => _truckInfo.BoostCost[_data.BoostBuyLevel].Cost;
    private int updateCost => _truckInfo.Upgrades[_data.UpdateLevel].SoftCost;

    public void OnSceneLoaded()
    {
        CalculateUpdateData();

        _playerProgress.Riches.Coins.Subscribe(Interactebles);
        _truckPanel.UpdateTruckButton.onClick.AddListener(UpdateTrucks);
        _truckPanel.BoostTruckButton.onClick.AddListener(BoostTrucks);
        _truckPanel.BuyTruckButtonsAddListener(BuyTruck);
        _data.BoostCount.Subscribe(_truckPanel.RenderBoost);
        _truckPanel.SubscribeResources(SetResource);

        RenderUpdateButton();

        _truckPanel.RenderBoost(_data.BoostCount.Value);
        _truckPanel.BoostButtonRefresh(boostCost);

        _truckPanel.ResourcesRefresh(_data.ResourceLevel);
        _truckPanel.ResourceChoise(_data.CurrentResource);

        _truckPanel.RenderCost(resourceCost);
        Interactebles();

        _truckZone.UpdateOn();
    }


    private void Interactebles() => Interactebles(_playerProgress.Riches.Coins.Value);
    public void Interactebles(int coinsCount)
    {
        bool isUpdatable = coinsCount >= updateCost;
        bool isBoostable = coinsCount >= boostCost;
        bool isBuyble = coinsCount >= resourceCost;

        _truckPanel.Interactebles(isUpdatable, isBoostable, isBuyble);
    }

    private void SetResource(int index)
    {
        _progressService.SetResource(index);
        _truckPanel.RenderCost(resourceCost);
        Interactebles();
    }

    private void CalculateUpdateData()
    {
        int capacity = 0, luck = 0, resources = 0;
        for(int i = 0; i < _data.UpdateLevel && i < _truckInfo.Upgrades.Length; i++)
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
        int level = _data.UpdateLevel;
        if (level >= _truckInfo.Upgrades.Length)
        {
            _truckPanel.UpdateButtonRefresh("MAX LEVEL", false);
            return;
        }

        TruckUpdates nextUpdate = _truckInfo.Upgrades[level];
        string text = $"+{nextUpdate.TruskUpdate} \n {nextUpdate.SoftCost}$";
        _truckPanel.UpdateButtonRefresh(text);
    }
    private void UpdateTrucks()
    {
        if (!_progressService.SpendCoins(updateCost))
            return;

        _progressService.UpdateTruck();
        CalculateUpdateData();
        RenderUpdateButton();
        _truckPanel.ResourcesRefresh(_data.ResourceLevel);
        Interactebles();
    }
    private void BoostTrucks()
    {
        if (!_progressService.SpendCoins(boostCost))
            return;

        if (_data.BoostBuyLevel + 1 < _truckInfo.BoostCost.Length)
            _data.BoostBuyLevel++;

        _progressService.AddBoost(_truckInfo.BoostLimit);

        _truckPanel.BoostButtonRefresh(boostCost);
        _truckZone.ChanceTruckSpeed(_truckInfo.MaxSpeed);
        Interactebles();
    }

    public void BuyTruck()
    {
        if(!_progressService.SpendCoins(resourceCost))
            return;

        Truck truck = new();
        truck.TruckCargo = new();

        TruckResources resource = _truckInfo.MergeItems[_data.CurrentResource];

        int index = Math.Min(resource.Resource.Length-1, _data.LuckLevel);
        LootBox lootBox = resource.Resource[index];

        for(int i = 0; i < _data.CargoCapacity; i++)
        {
            MergeItem mergeItem = lootBox.GetRandomItem<MergeItem>();
            truck.TruckCargo.Add(mergeItem);
        }

        AddNewTruck(truck);
        Interactebles();
    }

    public void AddNewTruck(Truck truck)
    {
        _progressService.EnqueueTruck(Prepare(truck));
        _truckZone.UpdateOn();
    }

    public Truck Dequeue()
    {
        TruckData truckData = _progressService.DequeueTruck();
        return Prepare(truckData);
    }
    public Truck Pop()
    {
        TruckData truck = _playerProgress.Trucks.ToArrive[0];
        return Prepare(truck);
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
    private Truck Prepare(TruckData truckData)
    {
        if(truckData == null)
            return null;

        Truck result = new Truck();
        result.TruckCargo = new();

        if (_data.BoostCount.Value > 0)
        {
            _progressService.RemoveBoost();
            result.Speed = _truckInfo.MaxSpeed;
        }
        else
            result.Speed = _truckInfo.MinSpeed;

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