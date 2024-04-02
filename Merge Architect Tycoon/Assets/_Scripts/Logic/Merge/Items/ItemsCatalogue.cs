using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ItemsCatalogue : MonoBehaviour
{
    public List<MergeItem> mergeItemsCatalogue;

    [Inject] SlotsManager slotsManager;
    [Inject] InformationPanel informationPanel;

    private int GetItemsCount(MergeItem item)
    {
        return slotsManager.Slots
            .FindAll(
                s => s.CurrentItem && s.CurrentItem.name == item.name && s.SlotState == SlotState.Draggable)
            .Count;
    }

    public int GetItemCount(MergeItem item)
    {
        return slotsManager.Slots
            .FindAll(
                s => s.CurrentItem && s.CurrentItem.name == item.name && s.SlotState == SlotState.Draggable)
            .Count;
    }

    public bool CheckHasItems(List<MergeItem> items)
    {
        foreach (var item in items.Distinct())
        {
            if (items.Select(x => x.name == item.name).Count() > GetItemsCount(item))
                return false;
        }

        return true;
    }

    public void TakeItems(List<MergeItem> items)
    {
        foreach (var item in items)
        {
            var slotItem = slotsManager.Slots.FirstOrDefault(s => s.CurrentItem &&
                                                                  s.CurrentItem.name == item.name &&
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
    }
}