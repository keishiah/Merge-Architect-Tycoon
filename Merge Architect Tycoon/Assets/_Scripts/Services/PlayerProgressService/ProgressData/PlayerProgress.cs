using System;
using UnityEngine.Serialization;

public class PlayerProgress
{
    public Action OnMergeItem;

    public PlayerStats PlayerStats { get; set; }
    public PlayerRiches Riches { get; set; }
    public BuildingsData Buldings { get; set; }
    public TutorialData Tutorial { get; set; }
    public InventoryData Inventory { get; set; }
    public QuestsData Quests { get; set; }
    public TrucksData Trucks { get; set; }

    public DistrictData DistrictData { get; set; }
}