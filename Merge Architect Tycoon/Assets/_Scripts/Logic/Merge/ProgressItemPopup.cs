using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressItemPopup : MonoBehaviour
{
    [SerializeField]
    private Categories[] categories;

    [SerializeField]
    private Image mainImage;
    [SerializeField]
    private Image[] stagesImage;
    [SerializeField]
    private TMP_Text mainText;
    [SerializeField]
    private Transform selectedCursor;

    public void OpenProgressItemInfo(MergeItem m_mergeItem)
    {
        gameObject.SetActive(true);

        mainImage.sprite = m_mergeItem.ItemSprite;
        mainText.text = m_mergeItem.ItemName + $"\n (Lvl {m_mergeItem.ItemLevel})";
        
        int i = 0;
        bool isFindCategory = false;
        for(; !isFindCategory; )
        {
            foreach(Sprite sprite in categories[i].sprites)
            {
                if (sprite.name == m_mergeItem.ItemSprite.name)
                    {
                        isFindCategory = true;
                        break;
                    }
            }
            if(!isFindCategory)
                i++;
        }

        for(int j = 0; j < stagesImage.Length; j++)
        {
            stagesImage[j].sprite = categories[i].sprites[j];
        }

        selectedCursor.position = stagesImage[m_mergeItem.ItemLevel-1].transform.position;
    }

    //private void Clear()
    //{
    //    if (itemDropSlotsList.Count > 0)
    //    {
    //        foreach (var item in itemDropSlotsList)
    //        {
    //            Destroy(item.gameObject);
    //        }

    //        itemDropSlotsList.Clear();
    //    }
    //}

    //private void OnDisable()
    //{
    //    foreach (var item in itemDropSlotsList)
    //    {
    //        Destroy(item.gameObject);
    //    }

    //    itemDropSlotsList.Clear();
    //}

    [Serializable]
    private class Categories
    {
        public Sprite[] sprites;
    }
}