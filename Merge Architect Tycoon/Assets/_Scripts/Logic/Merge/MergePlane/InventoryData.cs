using System;

public class InventoryData
{
    public int GridX, GridY;

    public Slot[] items;

    [Serializable]
    public struct Slot
    {
        public SlotState SlotState;
        public string ItemID;
    }
}