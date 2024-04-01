using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class QuestPerformanceItem : MonoBehaviour
    {
        public TextMeshProUGUI itemText;
        public Image performanceItemImage;
        public Image checkMarkImage;
        public TextMeshProUGUI performanceItemText;

        private void Start()
        {
            HideCompletedMark();
        }

        public void RenderItemPerformance(string itemName, Sprite itemImage, int currentItemCount, int itemsCount)
        {
            itemText.text = itemName;
            performanceItemImage.sprite = itemImage;
            performanceItemText.text = $"{itemName} {currentItemCount}/{itemsCount}";
        }

        public void RenderCompletedItemPerformance(string itemName, Sprite itemImage, int itemsCount)
        {
            itemText.text = itemName;
            performanceItemImage.sprite = itemImage;
            performanceItemText.text = $"{itemName} {itemsCount}/{itemsCount}";
        }


        public void ItemCompleted()
        {
            checkMarkImage.gameObject.SetActive(true);
        }

        public void HideCompletedMark() => checkMarkImage.gameObject.SetActive(false);
    }
}