using System.Collections.Generic;
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
    public void UpdateTruck()
    {
        _progress.Trucks.UpdateLevel++;
        SaveLoadService.Save(SaveKey.Truck, _progress.Trucks);
    }
    public void AddBoost(int count)
    {
        _progress.Trucks.BoostCount.Value += count;
        SaveLoadService.Save(SaveKey.Truck, _progress.Trucks);
    }
    public void RemoveBoost()
    {
        _progress.Trucks.BoostCount.Value--;
        SaveLoadService.Save(SaveKey.Truck, _progress.Trucks);
    }
    public void SetResource(int resourceIndex)
    {
        _progress.Trucks.CurrentResource = resourceIndex;
        SaveLoadService.Save(SaveKey.Truck, _progress.Trucks);
    }
    public void EnqueueTruck(TruckData newTtruck)
    {
        Queue<TruckData> trucks;
        if (_progress.Trucks.ToArrive != null)
            trucks = new Queue<TruckData>(_progress.Trucks.ToArrive);
        else
            trucks = new Queue<TruckData>();
        trucks.Enqueue(newTtruck);
        _progress.Trucks.ToArrive = trucks.ToArray();
        SaveLoadService.Save(SaveKey.Truck, _progress.Trucks);
    }
    public TruckData DequeueTruck()
    {
        if( _progress.Trucks.ToArrive.Length <= 0)
            return null;

        Queue<TruckData> trucks = new Queue<TruckData>(_progress.Trucks.ToArrive);
        TruckData truck = trucks.Dequeue();
        _progress.Trucks.ToArrive = trucks.ToArray();
        SaveLoadService.Save(SaveKey.Truck, _progress.Trucks);

        return truck;
    }
    public string DequeueTruckItem()
    {
        if (_progress.Trucks.ToArrive.Length <= 0
            || _progress.Trucks.ToArrive[0].Cargo.Length <= 0)
            return string.Empty;

        List<string> items = new List<string>(_progress.Trucks.ToArrive[0].Cargo);
        string itemID = items[^1];
        items.Remove(itemID);

        if (items.Count <= 0)
            DequeueTruck();
        else
        {
            _progress.Trucks.ToArrive[0].Cargo = items.ToArray();
            SaveLoadService.Save(SaveKey.Truck, _progress.Trucks);
        }

        return itemID;
    }
    #endregion

    #region Quests
    public void AddQuest(BaseQuestInfo questInfo)
    {
        _progress.Quests.ActiveQuests.Add(questInfo.GetNewQuestData());
        SaveLoadService.Save(SaveKey.Quests, _progress.Quests);
    }
    public void AddQuest(QuestData quest)
    {
        if (_progress.Quests.ActiveQuests.Contains(quest))
            return;

        _progress.Quests.ActiveQuests.Add(quest);
        SaveLoadService.Save(SaveKey.Quests, _progress.Quests);
    }
    public void QuestComplete(QuestData quest)
    {
        _progress.Quests.ActiveQuests.Remove(quest);
        _progress.Quests.CompletedQuests.Add(quest.QuestInfo.name);
        SaveLoadService.Save(SaveKey.Quests, _progress.Quests);
    }
    #endregion
    public void SaveAll()
    {
        SaveLoadService.Save(SaveKey.Riches, _progress.Riches);
        SaveLoadService.Save(SaveKey.Buldings, _progress.Buldings);
        SaveLoadService.Save(SaveKey.Quests, _progress.Quests);
        SaveLoadService.Save(SaveKey.Truck, _progress.Trucks);
    }
}
