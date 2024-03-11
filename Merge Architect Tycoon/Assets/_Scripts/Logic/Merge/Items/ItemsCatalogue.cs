using System;
using System.Collections.Generic;
using System.Linq;
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
        return slotsManager.Slots.FindAll(s => s.CurrentItem == item && s.SlotState == SlotState.Draggable).Count >
               0;
    }

    public bool CheckHasItems(List<MergeItem> items)
    {
        foreach (var item in items)
        {
            if (items.Select(x => x.itemName == item.itemName).Count() >
                slotsManager.Slots.Select(s =>
                        s.CurrentItem && s.CurrentItem.itemName == item.itemName && s.SlotState == SlotState.Draggable)
                    .Count()) return false;
        }

        return items.All(item =>
            slotsManager.Slots.Exists(s =>
                s.CurrentItem && s.CurrentItem.itemName == item.itemName && s.SlotState == SlotState.Draggable));
    }

    public void TakeItems(List<MergeItem> items)
    {
        List<Slot> slots = new List<Slot>();
        List<Slot> managersSlots = slotsManager.Slots;

        foreach (MergeItem item in items)
        {
            slots.Add(slotsManager.Slots.FirstOrDefault(s => s.CurrentItem &&
                                                             s.CurrentItem.itemName == item.itemName &&
                                                             s.SlotState == SlotState.Draggable));
        }

        if (slots.Count >= items.Count)
        {
            foreach (var slot in slots)
            {
                if (informationPanel.slotCurrent == slot)
                {
                    informationPanel.ActivateInfromPanel(false);
                }

                slot.RemoveItem();
            }
        }
        else
        {
            Debug.Log("Non Items");
        }
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