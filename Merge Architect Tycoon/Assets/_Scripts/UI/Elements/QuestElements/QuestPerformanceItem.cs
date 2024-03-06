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

        public TextMeshProUGUI performanceItemText;

        private int _itemToCreateCount;
        private int _currentToCreateCount;

        public void RenderBuildingQuestPerformance(string buildingToBuild, Sprite buildingImage)
        {
            itemText.text = buildingToBuild;
            performanceItemImage.sprite = buildingImage;
            performanceItemText.text = $"{buildingToBuild} 0/1";
        }

        public void RenderCreateItemQuestPerformance(MergeItem itemToCreate, int itemsCount)
        {
            itemText.text = itemToCreate.itemName;
            performanceItemImage.sprite = itemToCreate.itemSprite;
            performanceItemText.text = $"{itemToCreate.itemName} 0/{itemsCount}";
        }
    }
}