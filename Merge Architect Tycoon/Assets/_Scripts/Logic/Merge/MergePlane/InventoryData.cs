using System;
using UniRx;

public class InventoryData
{
    public int GridX, GridY;

    public Slot[] items;

    public ReactiveProperty<bool> InventoryFlag = new();

    [Serializable]
    public struct Slot
    {
        public SlotState SlotState;
        public string ItemID;
    }
}