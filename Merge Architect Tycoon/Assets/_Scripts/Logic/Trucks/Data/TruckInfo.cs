using UnityEngine;

[CreateAssetMenu(fileName = "TruckConfig", menuName = "StaticData/TruckConfig")]
public class TruckInfo : ScriptableObject
{
    public int CargoMinCount = 2;
    public int ResourcesMinCount = 1;

    public int MinSpeed;
    public int MaxSpeed;

    public TruckUpdates[] Upgrades;

    public int BoostLimit;
    public TruckBoostCost[] BoostCost;

    public TruckResources[] MergeItems;
    //public Sprite[] Sprites;
}
