using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TruckPanel : MonoBehaviour
{
    public Button MenuTruckButton;
    public Sprite[] RebuyTruckSprites;
    public Image RebuyTruckImage;

    public Button UpdateTruckButton;
    public TextMeshProUGUI UpdateTruckButtonText;

    public Button BoostTruckButton;
    public TextMeshProUGUI BoostTruckButtonText;
    public TextMeshProUGUI[] BoostTexts;

    public Button[] ResourceButtons;

    //public Button CloseTruckButton;
    public Button[] BuyTruckButtons;
    public TextMeshProUGUI PriceText;

    [SerializeField]
    private RectTransform _resourceCursor;

    public void RenderBoost(int count)
    {
        foreach (TextMeshProUGUI text in BoostTexts)
        {
            text.text = $"{count}";
        }
    }
    public void RenderCost(int price)
    {
        PriceText.text = $"-{price}$ / truck";
    }

    public void UpdateButtonRefresh(string text, bool interactable = true)
    {
        UpdateTruckButtonText.text = text;
        UpdateTruckButton.interactable = interactable;
    }

    public void BoostButtonRefresh(int i)
    {
        BoostTruckButtonText.text = $"BOOST ${i}";
    }

    public void ResourcesRefresh(int level)
    {
        for(int i = 0; i < ResourceButtons.Length; i++)
        {
            ResourceButtons[i].gameObject.SetActive(i<level);
        }
    }

    public void ResourceChoise(int i)
    {
        _resourceCursor.SetParent(ResourceButtons[i].transform, worldPositionStays: false);
    }

    public void SubscribeResources(Action<int> setResource)
    {
        for (int i = 0; i < ResourceButtons.Length; i++)
        {
            int index = i;//new instance
            ResourceButtons[i].onClick.AddListener(() => setResource(index));
        }
    }

    public void Interactebles(bool isUpdatable, bool isBoostable, bool isBuyble)
    {
        UpdateTruckButton.interactable = isUpdatable;
        BoostTruckButton.interactable = isBoostable;
        foreach(Button buyButton in BuyTruckButtons)
        {
            buyButton.interactable = isBuyble;
        }
    }

    public void BuyTruckButtonsAddListener(UnityAction buyTruck)
    {
        foreach (Button buyButton in BuyTruckButtons)
        {
            buyButton.onClick.AddListener(buyTruck);
        }
    }
}
