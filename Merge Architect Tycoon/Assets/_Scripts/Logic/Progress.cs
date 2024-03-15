using UnityEngine;

public class Progress
{
    public Coins Coins = new();
    public Coins Diamonds = new();
    public Buldings Buldings = new();

    public void AddCoins(int coins)
    {
        Coins.Add(coins);
        SaveLoadService.Save(SaveKey.Progress, this);
    }

    public void AddDiamonds(int coins)
    {
        Diamonds.Add(coins);
        SaveLoadService.Save(SaveKey.Progress, this);
    }

    public void AddBuilding(string buildingName)
    {
        Buldings.AddCreatedBuildingToList(buildingName);
        SaveLoadService.Save(SaveKey.Progress, this);
    }

    public void AddBuildingCreationRest(string buildingName, int creationRest)
    {
        Buldings.buildingsInProgress.AddBuildingProgress(buildingName, creationRest);
        SaveLoadService.Save(SaveKey.Progress, this);
    }
}