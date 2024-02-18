using System.Collections.Generic;
using CodeBase.Services.PlayerProgressService;
using UnityEngine;
using Zenject;

public class SlotsManager
{
    [SerializeField] private List<Slot> slots = new List<Slot>();


    [SerializeField] private int emptySlotsCount = 0;
    

    public int EmptySlotsCount
    {
        get { return slots.Count; }
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
    }

    public void AddItemToEmptySlot(MergeItem mergeItem)
    {
        if (emptySlotsCount == 0)
        {
            Debug.Log("No empty slots");
            return;
        }

        List<Slot> m_slotsList = Slots.FindAll(slot => slot.IsEmpty);

        Slot m_slot = m_slotsList[Random.Range(0, m_slotsList.Count)];

        if (CheckSlotIsEmpty(m_slot))
        {
            m_slot.AddItem(mergeItem);
            return;
        }
    }

    public bool CheckSlotIsEmpty(Slot slot)
    {
        return slot.IsEmpty;
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