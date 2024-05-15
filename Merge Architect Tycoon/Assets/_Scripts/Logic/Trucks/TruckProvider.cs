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
    [Inject] private AudioPlayer _audio;

    private TruckInfo _truckInfo => _staticDataService.TruckInfo;
    private TrucksData _data => _playerProgress.Trucks;

    private int resourceCost => _truckInfo.MergeItems[_data.CurrentResource].SoftCost;
    private int boostCost => _truckInfo.BoostCost.Cost;
    private int UpdateSoftCost()
    {
        if(_data.UpdateLevel >= _truckInfo.Upgrades.Length)
            return int.MaxValue;

        return _truckInfo.Upgrades[_data.UpdateLevel].SoftCost;
    }
    private int UpdateHardCost()
    {
        if (_data.UpdateLevel >= _truckInfo.Upgrades.Length)
            return int.MaxValue;

        return _truckInfo.Upgrades[_data.UpdateLevel].HardCost;
    }
    

    public void OnSceneLoaded()
    {
        CalculateUpdateData();

        _playerProgress.Riches.Coins.Subscribe(InteracteblesBySoft);
        _playerProgress.Riches.Diamonds.Subscribe(InteracteblesByHard);
        _truckPanel.UpdateTruckBySoftButton.onClick.AddListener(UpdateTrucksBySoft);
        _truckPanel.UpdateTruckByHardButton.onClick.AddListener(UpdateTrucksByHard);
        _truckPanel.BoostTruckButton.onClick.AddListener(BoostTrucks);
        _truckPanel.BuyTruckButtonsAddListener(BuyTruck);
        _data.BoostCount.Subscribe(_truckPanel.RenderBoost);

        RenderUpdateButton();

        _truckPanel.RenderBoost(_data.BoostCount.Value);
        _truckPanel.BoostButtonRefresh(boostCost);

        _truckPanel.RenderResourceCost(resourceCost);
        InteracteblesBySoft();
        InteracteblesByHard();

        _truckZone.UpdateOn();
    }

    private void InteracteblesBySoft() => InteracteblesBySoft(_playerProgress.Riches.Coins.Value);
    public void InteracteblesBySoft(int coinsCount)
    {
        bool isUpdatable = coinsCount >= UpdateSoftCost();
        bool isBuyble = coinsCount >= resourceCost;

        _truckPanel.InteracteblesBySoft(isUpdatable, isBuyble);
    }
    private void InteracteblesByHard() => InteracteblesByHard(_playerProgress.Riches.Diamonds.Value);
    public void InteracteblesByHard(int diamondsCount)
    {
        bool isUpdatable = diamondsCount >= UpdateHardCost();
        bool isBoostable = diamondsCount >= boostCost;

        _truckPanel.InteracteblesByHard(isUpdatable, isBoostable);
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
                case TruskUpdate.Luck:
                    luck++;
                    break;
                case TruskUpdate.Diversity:
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
            _truckPanel.UpdateButtonRefresh("MAX LEVEL");
            return;
        }

        TruckUpdates nextUpdate = _truckInfo.Upgrades[level];
        string text = $"+ {nextUpdate.TruskUpdate}";
        if (nextUpdate.TruskUpdate == TruskUpdate.Luck)
            text = $"+ Upgrade truck";
        _truckPanel.UpdateButtonRefresh(text, nextUpdate);
    }
    private void UpdateTrucksByHard()
    {
        int cost = UpdateHardCost();
        if (!_progressService.SpendDiamonds(cost))
        {
            int missingCurrency = cost - _playerProgress.Riches.Diamonds.Value;
            _truckPanel.ShowToShopPopup(missingCurrency);
            return;
        }

        UpdateTrucks();
    }
    private void UpdateTrucksBySoft()
    {
        int cost = UpdateSoftCost();
        if (!_progressService.SpendCoins(UpdateSoftCost()))
        {
            int missingCurrency = cost - _playerProgress.Riches.Coins.Value;
            _truckPanel.ShowToShopPopup(missingCurrency, isHardCurrency: false);
            return;
        }

        UpdateTrucks(); 
    }

    private void UpdateTrucks()
    {
        _audio.PlayUiSound(UiSoundTypes.BuyUpdate);
        _progressService.UpdateTruck();
        CalculateUpdateData();
        RenderUpdateButton();
        InteracteblesBySoft();
        InteracteblesByHard();
    }
    private void BoostTrucks()
    {
        if (!_progressService.SpendDiamonds(boostCost))
        {
            int missingCurrency = boostCost - _playerProgress.Riches.Diamonds.Value;
            _truckPanel.ShowToShopPopup(missingCurrency);
            return;
        }

        _audio.PlayUiSound(UiSoundTypes.BuyUpdate);
        //if (_data.BoostBuyLevel + 1 < _truckInfo.BoostCost.Length)
        //    _data.BoostBuyLevel++;

        _progressService.AddBoost(_truckInfo.BoostLimit);

        _truckPanel.BoostButtonRefresh(boostCost);
        _truckZone.ChanceTruckSpeed(_truckInfo.MaxSpeed);
        InteracteblesByHard();
    }

    public void BuyTruck()
    {
        if (_playerProgress.Trucks.ToArrive != null && _playerProgress.Trucks.ToArrive.Length > 0)
            return;

        if(!_progressService.SpendCoins(resourceCost))
        {
            int missingCurrency = resourceCost - _playerProgress.Riches.Coins.Value;
            _truckPanel.ShowToShopPopup(missingCurrency, isHardCurrency: false);
            return;
        }

        if (resourceCost > 0)
            _audio.PlayUiSound(UiSoundTypes.BuyUpdate);

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
        InteracteblesBySoft();
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
