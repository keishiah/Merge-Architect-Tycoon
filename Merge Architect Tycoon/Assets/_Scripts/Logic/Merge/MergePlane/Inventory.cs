using System;

namespace _Scripts.Logic.Merge.MergePlane
{
    public class Inventory
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
}