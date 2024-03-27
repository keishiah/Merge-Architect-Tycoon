using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRenderer : MonoBehaviour
{
    public Image buildingStateImage;
    public Image buildInProcessImage;
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
        Destroy(timerText.gameObject);
    }

    public void UpdateTimerText(string formattedTime)
    {
        timerText.text = formattedTime;
    }

    public void ShowBuildSprite(Sprite spriteToShow)
    {
        buildInProcessImage.transform.parent.gameObject.SetActive(false);
        buildingStateImage.gameObject.SetActive(true);
        buildingStateImage.sprite = spriteToShow;
    }

    public void ShowBuildInProgressSprite(Sprite spriteToShow)
    {
        buildInProcessImage.transform.parent.gameObject.SetActive(true);
        buildInProcessImage.sprite = spriteToShow;
    }
}