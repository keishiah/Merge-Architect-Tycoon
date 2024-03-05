using System;
using System.Collections.Generic;
using UnityEngine;

public class MergeItemsManager
{
    public List<ItemGeneralOpenedStruct> itemGeneralOpenedStructsList;
    public List<MergeItem> mergeItems = new();

    public bool GetItemOpenedInfo(MergeItem m_mergeItem)
    {
        return itemGeneralOpenedStructsList.Exists(x => x.itemName == m_mergeItem.name);
    }

    public void AddOpenedItem(MergeItem m_mergeItem)
    {
        if (!itemGeneralOpenedStructsList.Exists(x => x.itemName == m_mergeItem.name))
        {

            ItemGeneralOpenedStruct itemGeneral = new ItemGeneralOpenedStruct();

            itemGeneral.itemName = m_mergeItem.name;
            itemGeneral.isOpened = true;
            itemGeneralOpenedStructsList.Add(itemGeneral);
            SaveItemGeneralOpened();
        }
    }

    private void SaveItemGeneralOpened()
    {
        string content = string.Empty;

        for (int i = 0; i < itemGeneralOpenedStructsList.Count; i++)
        {
            ItemGeneralOpenedStruct slot = itemGeneralOpenedStructsList[i];
            content += slot.itemName + "-" + slot.isOpened + ";";
        }
        PlayerPrefs.SetString("itemGeneralOpened", content);
    }

    public void LoadItemGeneralOpened()
    {
        mergeItems.AddRange(Resources.LoadAll<MergeItem>(AssetPath.Items));
        if (PlayerPrefs.HasKey("itemGeneralOpened"))
        {
            string content = PlayerPrefs.GetString("itemGeneralOpened");
            string[] splitedContent = content.Split(';');

            for (int i = 0; i < splitedContent.Length - 1; i++)
            {
                string[] splitedValue = splitedContent[i].Split('-');
                //int index = int.Parse(splitedValue[0]);
                string name = splitedValue[0];
                bool slotState = bool.Parse(splitedValue[1]);

                ItemGeneralOpenedStruct itemGeneral = new ItemGeneralOpenedStruct();

                itemGeneral.itemName = mergeItems.Find(x => x.name == name).name;
                itemGeneral.isOpened = true;

                itemGeneralOpenedStructsList.Add(itemGeneral);
            }
        }
    }
}

[Serializable]
public struct ItemGeneralOpenedStruct
{
    public string itemName;
    public bool isOpened;
}