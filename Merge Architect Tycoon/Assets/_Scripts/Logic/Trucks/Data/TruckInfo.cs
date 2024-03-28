using UnityEngine;

[CreateAssetMenu(fileName = "TruckConfig", menuName = "StaticData/TruckConfig")]
public class TruckInfo : ScriptableObject
{
    public TruckUpdates[] Upgrades;
    public int BoostLimit;
    public TruckResources[] MergeItems;
    //public Sprite[] Sprites;
}
