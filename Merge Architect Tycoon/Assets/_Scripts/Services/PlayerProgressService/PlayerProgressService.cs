using System;
using Zenject;

public class PlayerProgressService
{
    [Inject] private PlayerProgress _progress;

    #region Rich
    public void AddCoins(int coins)
    {
        _progress.Riches.Coins.Value += coins;
        SaveLoadService.Save(SaveKey.Riches, _progress.Riches);
    }
    public void AddDiamonds(int coins)
    {
        _progress.Riches.Diamonds.Value += coins;
        SaveLoadService.Save(SaveKey.Riches, _progress.Riches);
    }
    public bool SpendCoins(int coinsToSpend)
    {
        if(_progress.Riches.Coins.Value >= coinsToSpend)
        {
            _progress.Riches.Coins.Value -= coinsToSpend;
            return true;
        }

        return false;
    }
    public bool SpendDiamonds(int diamondsToSpend)
    {
        if (_progress.Riches.Diamonds.Value >= diamondsToSpend)
        {
            _progress.Riches.Diamonds.Value -= diamondsToSpend;
            return true;
        }

        return false;
    }
    #endregion

    #region Buildings
    public void AddBuilding(string buildingName)
    {
        _progress.Buldings.AddCreatedBuildingToList(buildingName);
        SaveLoadService.Save(SaveKey.Buldings, _progress.Buldings);
    }

    public void AddBuildingCreationTimeRest(string buildingName, int creationRest)
    {
        _progress.Buldings.buildingsInProgress.AddBuildingProgress(buildingName, creationRest);
        SaveLoadService.Save(SaveKey.Buldings, _progress.Buldings);
    }

    public void RemoveBuildingInProgress(string buildingName)
    {
        _progress.Buldings.buildingsInProgress.RemoveBuildingProgress(buildingName);
        SaveLoadService.Save(SaveKey.Buldings, _progress.Buldings);
    }
    #endregion

    #region Trucks
    public void AddBoostTrucks(int count)
    {
        _progress.Trucks.BoostCount.Value = count;
        SaveLoadService.Save(SaveKey.Truck, _progress.Trucks);
    }
    public void RemoveBoostTrucks()
    {
        _progress.Trucks.BoostCount.Value--;
        SaveLoadService.Save(SaveKey.Truck, _progress.Trucks);
    }

    #endregion

    #region Quests

    #endregion
    public void SaveAll()
    {
        SaveLoadService.Save(SaveKey.Riches, _progress.Riches);
        SaveLoadService.Save(SaveKey.Buldings, _progress.Buldings);
        SaveLoadService.Save(SaveKey.Quests, _progress.Quests);
        SaveLoadService.Save(SaveKey.Truck, _progress.Trucks);
    }
}
