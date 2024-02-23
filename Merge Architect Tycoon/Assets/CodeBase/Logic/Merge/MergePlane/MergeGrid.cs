using System;
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
            PlayerPrefs.DeleteAll();
            level.isNeedResetLevel = false;
        }

        mergeItemsGeneralOpened.LoadItemGeneralOpened();
        Input.multiTouchEnabled = false;
        LoadInventory();
    }

    private void CreateLayout()
    {
        int slotsColumns = level.columns;
        int slotsRows = level.rows;

        int allSlots = slotsColumns * slotsRows;
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();

        gridLayoutGroup.constraintCount = slotsColumns;

        for (int i = 0; i < allSlots; i++)
        {
            int slot_x = i % slotsColumns;
            int slot_y = i / slotsColumns;

            Slot initSlot = _container.InstantiatePrefabForComponent<Slot>(slotPrefab);

            initSlot.addItemEvent += SaveInventory;
            initSlot.removeItemEvent += SaveInventory;

            RectTransform slotRect = initSlot.GetComponent<RectTransform>();

            initSlot.SlotID = i;
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
        StringBuilder contentBuilder = new StringBuilder();

        for (int i = 0; i < slotsManager.Slots.Count; i++)
        {
            Slot slot = slotsManager.Slots[i];
            if (!slot.IsEmpty)
            {
                string itemName = slot.CurrentItem == null ? EMPTY_ITEM_NAME : slot.CurrentItem.name;

                contentBuilder.Append(i)
                    .Append("-")
                    .Append(itemName)
                    .Append("-")
                    .Append((int)slot.SlotState)
                    .Append(";");
            }

            var content = contentBuilder.ToString();

            PlayerPrefs.SetString("mergeContent", content);
            PlayerPrefs.Save();
        }
    }

    public void LoadInventory()
    {
        CreateLayout();

        bool isLoadSuccess = false;

        if (PlayerPrefs.HasKey("mergeContent"))
        {
            string content = PlayerPrefs.GetString("mergeContent");
            try
            {
                string[] splitedContent = content.Split(';');

                for (int i = 0; i < splitedContent.Length - 1; i++)
                {
                    string[] splitedValue = splitedContent[i].Split('-');
                    int index = int.Parse(splitedValue[0]);
                    int slotState = int.Parse(splitedValue[2]);
                    slotsManager.Slots[index].ChangeState((SlotState)slotState);
                    if (splitedValue[1] != EMPTY_ITEM_NAME)
                        slotsManager.Slots[index].AddItem(
                            Resources.Load<MergeItem>($"Items/{splitedValue[1]}"));
                }

                isLoadSuccess = true;
            }
            catch (Exception ex)
            {
                Debug.Log("Something went wrong");
                Debug.LogException(ex);
            }
        }

        if (isLoadSuccess)
            slotsManager.InitialItems();
        else
            slotsManager.InitialItems(level.allDropSlots);
    }
}