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

    private int GetItemsCount(MergeItem item)
    {
        return slotsManager.Slots
            .FindAll(
                s => s.CurrentItem && s.CurrentItem.itemName == item.itemName && s.SlotState == SlotState.Draggable)
            .Count;
    }

    public bool CheckHasItems(List<MergeItem> items)
    {
        foreach (var item in items.Distinct())
        {
            if (items.Select(x => x.itemName == item.itemName).Count() > GetItemsCount(item))
                return false;
        }

        return true;
    }

    public void TakeItems(List<MergeItem> items)
    {
        foreach (var item in items)
        {
            var slotItem = slotsManager.Slots.FirstOrDefault(s => s.CurrentItem &&
                                                                  s.CurrentItem.itemName == item.itemName &&
                                                                  s.SlotState == SlotState.Draggable);
            if (slotItem != null)
            {
                if (informationPanel.slotCurrent == slotItem)
                {
                    informationPanel.ActivateInfromPanel(false);
                }

                slotItem.RemoveItem();
            }
        }
        // List<Slot> slots = new List<Slot>();
        //
        // foreach (MergeItem item in items)
        // {
        //     slots.Add(slotsManager.Slots.FirstOrDefault(s => s.CurrentItem &&
        //                                                      s.CurrentItem.itemName == item.itemName &&
        //                                                      s.SlotState == SlotState.Draggable));
        // }
        //
        // if (slots.Count >= items.Count)
        // {
        //     foreach (var slot in slots)
        //     {
        //         if (informationPanel.slotCurrent == slot)
        //         {
        //             informationPanel.ActivateInfromPanel(false);
        //         }
        //
        //         slot.RemoveItem();
        //     }
        // }
        // else
        // {
        //     Debug.Log("Non Items");
        // }
    }

    public void AddItem(MergeItem m_item, SlotRenderer m_slot)
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