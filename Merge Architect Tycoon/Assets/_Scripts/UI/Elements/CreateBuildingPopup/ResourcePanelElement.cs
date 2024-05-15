using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ResourcePanelElement : MonoBehaviour
{
    public Image resourceImage;
    public TextMeshProUGUI resourceName;
    public TextMeshProUGUI resourceCountText;

    public Image checkMark;

    public void RenderElement(string itemName, Sprite image, int currentCount, int resourceGoal)
    {
        resourceImage.sprite = image;
        resourceName.text = itemName;
        RenderResourcesText(currentCount, resourceGoal);
    }

    private void RenderResourcesText(int currentCount, int resourceGoal)
    {
        resourceCountText.transform.parent.gameObject.SetActive(true);

        if (currentCount >= resourceGoal)
        {
            // resourceCountText.text = $"{resourceGoal}/{resourceGoal}";
            resourceCountText.transform.parent.gameObject.SetActive(false);
            checkMark.gameObject.SetActive(true);
        }
        else
        {
            resourceCountText.text = $"{currentCount}/{resourceGoal}";
            checkMark.gameObject.SetActive(false);
        }
    }


}