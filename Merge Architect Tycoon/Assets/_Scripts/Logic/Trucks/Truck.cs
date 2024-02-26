using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Truck
{
    public Sprite SpriteImage;
    public List<MergeItem> TruckCargo;

    public Truck Clone()
    {
        List<MergeItem> _truckCargo = new();
        foreach (var item in TruckCargo)
        {
            _truckCargo.Add(item);
        }

        Truck clone = new Truck();
        clone.SpriteImage = this.SpriteImage;
        clone.TruckCargo = _truckCargo;

        return clone;
    }

    public MergeItem DequeueItem()
    {
        if(TruckCargo == null || TruckCargo.Count == 0)
            return null;

        MergeItem item = TruckCargo[0];
        TruckCargo.Remove(item);
        return item;
    }
}