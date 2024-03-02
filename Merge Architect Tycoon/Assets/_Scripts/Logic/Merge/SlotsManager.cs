using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlotsManager
{
    [SerializeField] private List<Slot> slots = new List<Slot>();

    [SerializeField] private int emptySlotsCount = 0;

    public Action OnNewEmptySlotAppears;

    public int EmptySlotsCount
    {
        get { return emptySlotsCount; }
    }
    public int EmptyUnloadSlotsCount
    {
        get
        {
            return 
                Slots.FindAll(slot => slot.IsEmpty && slot.SlotState == SlotState.Unloading)
                .Count; 
        }
    }

    public List<Slot> Slots
    {
        get { return slots; }
    }

    public void InitialItems(List<ItemDropSlot> allDropSlots = null)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (slots[i].IsEmpty) emptySlotsCount++;
            InitEvents(i);

            if (allDropSlots != null)
            {
                slots[i].ChangeState(allDropSlots[i].slotState);
                slots[i].AddItem(allDropSlots[i].mergeItem);
            }
        }
    }

    public void InitNeighbours(int slotsColumns)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            int slot_x = i % slotsColumns;
            int slot_y = i / slotsColumns;
            List<Slot> neighbours = new();

            if (slot_x > 0) //left
                neighbours.Add(slots[i - 1]);
            if (slot_x < slotsColumns - 1) //right
                neighbours.Add(slots[i + 1]);

            if (slot_y > 0) //up
                neighbours.Add(slots[i - slotsColumns]);
            if (i < slots.Count - slotsColumns) //down
                neighbours.Add(slots[i + slotsColumns]);

            slots[i].SetNeighbours(neighbours.ToArray());
        }
    }

    private void InitEvents(int i)
    {
        slots[i].addItemEvent += () => { emptySlotsCount--; };
        slots[i].removeItemEvent += () => { emptySlotsCount++; };
        slots[i].endMoveEvent += () => 
        { 
            if(emptySlotsCount > 0) 
                OnNewEmptySlotAppears?.Invoke(); 
        };
    }

    public void AddItemToEmptySlot(MergeItem mergeItem, bool isToUnloadSlot = false)
    {
        SlotState slotState = isToUnloadSlot ? SlotState.Unloading : SlotState.Draggable;

        List<Slot> m_slotsList = Slots.FindAll(slot => slot.IsEmpty && slot.SlotState == slotState);

        if (m_slotsList.Count == 0)
        {
            Debug.Log("No empty slots");
            return;
        }

        Slot m_slot;
        if (isToUnloadSlot)
            m_slot = slots[0];//For truck no random
        else
            m_slot = m_slotsList[Random.Range(0, m_slotsList.Count)];

        if (m_slot.IsEmpty)
            m_slot.AddItem(mergeItem);
    }

    public void RemoveItem(MergeItem item)
    {
        Slot slot = slots.Find(x => x.CurrentItem == item);
        slot.RemoveItem();
    }

    public void RemoveAllItems()
    {
        foreach (Slot slot in slots)
        {
            slot.RemoveItem();
            slot.ChangeState(SlotState.Draggable);
        }
    }
}