public class Progress
{
    public Coins Coins = new();
    public Coins Diamonds = new();
    public Buldings Buldings = new();
    public Quests Quests = new();

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

    // public void AddActiveQuest(string questId)
    // {
    //     Quests.AddActiveQuest(questId);
    //     SaveLoadService.Save(SaveKey.Progress, this);
    // }

    // public void AddCompletedQuest(string questId)
    // {
    //     Quests.AddCompletedQuest(questId);
    //     SaveLoadService.Save(SaveKey.Progress, this);
    // }

    // public void AddQuestWaitingForClaim(string questId)
    // {
    //     Quests.AddQuestWaitingForClaim(questId);
    //     SaveLoadService.Save(SaveKey.Progress, this);
    // }

    // public void AddMergeItem()
    // {
    //     Quests.AddMergeItem();
    //     SaveLoadService.Save(SaveKey.Progress, this);
    // }

    public void ClearMergeCount()
    {
        Quests.ClearMergeCount();
        SaveLoadService.Save(SaveKey.Progress, this);
    }
}