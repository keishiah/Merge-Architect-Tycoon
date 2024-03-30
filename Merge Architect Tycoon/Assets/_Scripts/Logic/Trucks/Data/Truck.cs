using System;
using System.Collections.Generic;

[Serializable]
public class Truck
{
    //public Sprite SpriteImage;
    public List<MergeItem> TruckCargo;
    public Action DequeueAction;
    public int Speed;

    public MergeItem DequeueItem()
    {
        if(TruckCargo == null || TruckCargo.Count == 0)
            return null;

        MergeItem item = TruckCargo[TruckCargo.Count - 1];
        TruckCargo.Remove(item);

        DequeueAction?.Invoke();

        return item;
    }
}