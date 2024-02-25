using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Truck
{
    public Sprite SpriteImage;
    public List<MergeItem> TruckCargo;
    public MergeItem DequeueItem()
    {
        if(TruckCargo == null || TruckCargo.Count == 0)
            return null;

        MergeItem item = TruckCargo[0];
        TruckCargo.Remove(item);
        return item;
    }
}