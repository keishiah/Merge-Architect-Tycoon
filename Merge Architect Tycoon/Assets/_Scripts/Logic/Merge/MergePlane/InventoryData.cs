using System;

public class InventoryData
{
    public int GridX, GridY;

    public Slot[] items;

    public Action OnInventoryChanged;

    [Serializable]
    public struct Slot
    {
        public SlotState SlotState;
        public string ItemID;
    }
}