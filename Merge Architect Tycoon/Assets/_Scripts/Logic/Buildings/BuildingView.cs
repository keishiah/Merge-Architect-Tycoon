using CodeBase.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        public Image buildingStateImage;
        public TextMeshProUGUI timerText;

        public void SetViewInactive()
        {
            buildingStateImage.raycastTarget = false;
            timerText.gameObject.SetActive(false);
            buildingStateImage.gameObject.SetActive(false);
        }
        
        public void SetViewBuildInProgress()
        {
            buildingStateImage.raycastTarget = false;
            timerText.gameObject.SetActive(true);
        }

        public void SetViewBuildCreated()
        {
            buildingStateImage.raycastTarget = false;
            timerText.gameObject.SetActive(false);
        }

        public void UpdateTimerText(string formattedTime)
        {
            timerText.text = formattedTime;
        }

        public void ShowBuildSprite(Sprite spriteToShow)
        {
            buildingStateImage.gameObject.SetActive(true);
            buildingStateImage.sprite = spriteToShow;
        }
    }
}