using System;
using System.Collections.Generic;
using _Scripts.Logic.Merge.Items;
using UnityEngine;

namespace _Scripts.Logic.Merge.MergePlane
{
    [CreateAssetMenu(fileName = "MergeLevel", menuName = "ScriptableObjects/MergeLevel")]
    public class MergeLevel : ScriptableObject
    {
        [SerializeField]
        public bool isNeedResetLevel;

        public int columns = 5;
        public int rows = 5;

        public List<ItemDropSlot> allDropSlots;
    }

    [Serializable]
    public class ItemDropSlot
    {
        public MergeItem mergeItem;
        public SlotState slotState = SlotState.Blocked;
    }
}