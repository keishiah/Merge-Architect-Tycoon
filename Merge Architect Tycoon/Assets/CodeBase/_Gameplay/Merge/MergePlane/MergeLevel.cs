using System;
using System.Collections.Generic;
using UnityEngine;

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
