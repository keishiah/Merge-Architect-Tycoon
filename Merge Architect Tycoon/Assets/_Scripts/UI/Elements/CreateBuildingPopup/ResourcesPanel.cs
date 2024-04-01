using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourcesPanel : MonoBehaviour
{
    public Button actionButton;
    public List<ResourcePanelElement> resourcePanelElements;
    public CoinsPannelElement coinsElement;

    public void RenderResourceElement(string itemName, Sprite resourceImage, int resourceCount, int resourceGoal)
    {
        HideAllResources();
        var element = GetInactiveResourceElement();
        element.gameObject.SetActive(true);
        element.RenderElement(itemName, resourceImage, resourceCount, resourceGoal);
    }

    public void RenderCoinsCount(int currentCount, int coinsGoal)
    {
        coinsElement.gameObject.SetActive(true);
        coinsElement.RenderCoinsElement(currentCount, coinsGoal);
    }

    private ResourcePanelElement GetInactiveResourceElement()
    {
        return resourcePanelElements.Find(x => !x.gameObject.activeSelf);
    }

    private void HideAllResources()
    {
        foreach (var resourceElement in resourcePanelElements)
        {
            resourceElement.gameObject.SetActive(false);
        }
    }
}