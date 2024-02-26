using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemsCatalogue : MonoBehaviour
{
    public List<MergeItem> mergeItemsCatalogue;

    [Inject] SlotsManager slotsManager;
    [Inject] InformationPanel informationPanel;
    [Inject] MergeItemsManager mergeItemsGeneralOpenedManager;

    public bool CheckHasItem(MergeItem item)
    {
        return slotsManager.Slots.FindAll(s => s.CurrentItem == item && s.SlotState == SlotState.Draggable).Count > 0;
        // int counter = 0;
        // List<Slot> slots = new List<Slot>();
        //
        // slots.AddRange(slotsManager.Slots.FindAll(s => s.CurrentItem == item && s.SlotState == SlotState.Draggable));
        // counter = slots.Count;
        // return counter;
    }

    public void TakeItems(MergeItem item, int count)
    {
        List<Slot> slots = new List<Slot>();

        slots.AddRange(slotsManager.Slots.FindAll(s => s.CurrentItem == item && s.SlotState == SlotState.Draggable));

        Debug.Log(slots.Count);

        if (slots.Count >= count)
            for (int i = 0; i < count; i++)
            {
                if (informationPanel.slotCurrent == slots[i])
                {
                    informationPanel.ActivateInfromPanel(false);
                }

                slots[i].RemoveItem();
            }
        else
            Debug.Log("Non Items");
    }

    public void AddItem(MergeItem m_item, Slot m_slot)
    {
        mergeItemsCatalogue.Add(m_item);
        if (m_slot.SlotState != SlotState.Blocked)
            mergeItemsGeneralOpenedManager.AddOpenedItem(m_item);
    }

    public void RemoveItem(MergeItem item)
    {
        mergeItemsCatalogue.Remove(item);
    }
}