using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressItemPopup : MonoBehaviour
{
    [SerializeField]
    private ItemInfoSlot infoSlotPrefab;
    [SerializeField]
    private TMP_Text mainText;
    [SerializeField]
    private Transform gridPanel;

    private List<ItemInfoSlot> itemDropSlotsList = new List<ItemInfoSlot>();

    public void OpenProgressItemInfo(MergeItem m_mergeItem)
    {
        GetComponent<Image>().sprite = m_mergeItem.ItemSprite;
        //string[] name = m_mergeItem.name.Split('_');

        //mainText.text = name[0];

        //List<MergeItem> list = new List<MergeItem>();
        //list.AddRange(generalOopenedManager.mergeItems.FindAll(x => x.name.Split('_')[0] == name[0]));

        //for (int i = 0; i < list.Count; i++)
        //{
        //    ItemInfoSlot iis = Instantiate(infoSlotPrefab, gridPanel);
        //    iis.SetItem(list[i], generalOopenedManager.GetItemOpenedInfo(list[i]));
        //    itemDropSlotsList.Add(iis);
        //}

        gameObject.SetActive(true);
    }

    private void Clear()
    {
        if (itemDropSlotsList.Count > 0)
        {
            foreach (var item in itemDropSlotsList)
            {
                Destroy(item.gameObject);
            }

            itemDropSlotsList.Clear();
        }
    }

    private void OnDisable()
    {
        foreach (var item in itemDropSlotsList)
        {
            Destroy(item.gameObject);
        }

        itemDropSlotsList.Clear();
    }
}