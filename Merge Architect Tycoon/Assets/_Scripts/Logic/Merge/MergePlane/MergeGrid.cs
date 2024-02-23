using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(GridLayoutGroup))]
public class MergeGrid : MonoBehaviour
{
    private const string EMPTY_ITEM_NAME = "Empty";

    [SerializeField] public InformationPanel informationPanel;

    [SerializeField] private Slot slotPrefab;
    [Inject] public SlotsManager slotsManager;

    [Inject] private DiContainer _container;
    [Inject] private MergeLevel level;
    [Inject] private MergeItemsManager mergeItemsGeneralOpened;

    public void InitializeGrid()
    {
        if (level.isNeedResetLevel)
        {
            PlayerPrefs.DeleteKey(SaveKey.Inventory.ToString());
            level.isNeedResetLevel = false;
        }

        mergeItemsGeneralOpened.LoadItemGeneralOpened();
        Input.multiTouchEnabled = false;
        LoadInventory();
    }

    private void CreateLayout(int slotsColumns, int slotsRows)
    {
        int allSlots = slotsColumns * slotsRows;
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();

        gridLayoutGroup.constraintCount = slotsColumns;

        for (int i = 0; i < allSlots; i++)
        {
            int slot_x = i % slotsColumns;
            int slot_y = i / slotsColumns;

            Slot initSlot = _container.InstantiatePrefabForComponent<Slot>(slotPrefab);

            initSlot.saveEvent += SaveInventory;

            RectTransform slotRect = initSlot.GetComponent<RectTransform>();

            initSlot.name = $"Slot: {slot_y}_{slot_x} ID: {i}";
            initSlot.transform.SetParent(transform);

            slotRect.localPosition = Vector3.zero;
            slotRect.localRotation = Quaternion.Euler(Vector3.zero);

            slotRect.localScale = Vector3.one;

            slotsManager.Slots.Add(initSlot);
        }

        slotsManager.InitNeighbours(slotsColumns);
    }

    public void SaveInventory()
    {
        List<Inventory.Slot> itemList = new();

        for (int i = 0; i < slotsManager.Slots.Count; i++)
        {
            Slot slot = slotsManager.Slots[i];

            Inventory.Slot slotToSave = new()
            {
                SlotState = slot.SlotState,
                ItemID = slot.CurrentItem == null ? EMPTY_ITEM_NAME : slot.CurrentItem.name,
            };

            itemList.Add(slotToSave);
        }

        Inventory saveData = new()
        {
            GridX = level.columns,
            GridY = level.rows,
            items = itemList.ToArray()
        };

        SaveLoadService.Save(SaveKey.Inventory, saveData);
    }

    public void LoadInventory()
    {
        Inventory loadData = SaveLoadService.Load<Inventory>(SaveKey.Inventory);
        if (loadData == null || loadData.items == null)
        {
            CreateNewLevel();
            return;
        }

        bool isLoadSuccess = false;
        CreateLayout(loadData.GridX, loadData.GridY);
        try
        {
            var items = loadData.items;
            for (int i = 0; i < items.Length; i++)
            {
                slotsManager.Slots[i].ChangeState(items[i].SlotState);
                if (items[i].ItemID != EMPTY_ITEM_NAME)
                    slotsManager.Slots[i].AddItem(
                        Resources.Load<MergeItem>(AssetPath.Items + items[i].ItemID));
            }

            isLoadSuccess = true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"LoadInventory exeption: {ex.Message}");
        }

        if (isLoadSuccess)
            slotsManager.InitialItems();
        else
            slotsManager.InitialItems(level.allDropSlots);
    }

    private void CreateNewLevel()
    {
        CreateLayout(level.columns, level.rows);
        slotsManager.InitialItems(level.allDropSlots);
    }
}