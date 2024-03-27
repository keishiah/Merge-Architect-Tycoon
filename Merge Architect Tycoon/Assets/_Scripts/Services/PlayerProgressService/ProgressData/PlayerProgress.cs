using System;

public class PlayerProgress
{
    public Action OnMergeItem;

    public PlayerRiches Riches { get; set; }
    public BuildingsData Buldings { get; set; }
    public TutorialData Tutorial { get; set; }
    public InventoryData Inventory { get; set; }
    public QuestsData Quests { get; set; }
    public TruckData Trucks { get; set; }
}