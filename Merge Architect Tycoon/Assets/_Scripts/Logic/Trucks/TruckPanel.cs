using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TruckPanel : MonoBehaviour
{
    [Header("Buy truck")]
    public Sprite[] RebuyTruckSprites;
    public Button BuyTruckButtons;
    public TextMeshProUGUI PriceText;
    public Image RebuyTruckImage;
    [Header("Update")]
    public Sprite[] UpdateTruckSprites;
    public GameObject UpdatePanel;
    public Button UpdateTruckBySoftButton;
    public Button UpdateTruckByHardButton;
    public TextMeshProUGUI UpdateTruckButtonText;
    public Image UpdateTruckImage;
    [Header("Boost")]
    public Sprite[] BoostTruckSprites;
    public GameObject BoostPanel;
    public Button BoostTruckButton;
    public TextMeshProUGUI BoostTruckButtonText;
    public TextMeshProUGUI BoostTexts;
    public Image BoostTruckImage;

    [Header("Else")]
    public Button CloseTruckButton;

    public void RenderBoost(int count)
    {
        BoostTexts.text = $"{count}";
    }
    public void RenderCost(int price)
    {
        PriceText.text = $"-{price}";
    }

    public void UpdateButtonRefresh(string text, bool interactable = true)
    {
        UpdateTruckButtonText.text = text;
        UpdateTruckBySoftButton.interactable = interactable;
    }

    public void BoostButtonRefresh(int i)
    {
        BoostTruckButtonText.text = $"BOOST ${i}";
    }

    public void Interactebles(bool isUpdatable, bool isBoostable, bool isBuyble)
    {
        BuyTruckButtons.interactable = isBuyble;
        RebuyTruckImage.sprite = isBuyble ? RebuyTruckSprites[0] : RebuyTruckSprites[1];

        UpdateTruckBySoftButton.interactable = isUpdatable;
        UpdateTruckImage.sprite = isUpdatable ? UpdateTruckSprites[0] : UpdateTruckSprites[1];

        BoostTruckButton.interactable = isBoostable;
        BoostTruckImage.sprite = isBoostable ? BoostTruckSprites[0] : BoostTruckSprites[1];
    }

    public void BuyTruckButtonsAddListener(UnityAction buyTruck)
    {
        BuyTruckButtons.onClick.AddListener(buyTruck);
    }

    public void OpenUpdatePopup()
    {
        BoostPanel.SetActive(false);
        
        if (UpdateTruckImage.sprite == UpdateTruckSprites[0])
        {
            UpdateTruckImage.sprite = UpdateTruckSprites[2];
            UpdatePanel.SetActive(true);
        }
        else if (UpdateTruckImage.sprite == UpdateTruckSprites[1])
        {
            UpdateTruckImage.sprite = UpdateTruckSprites[3];
            UpdatePanel.SetActive(true);
        }
        else if (UpdateTruckImage.sprite == UpdateTruckSprites[2])
        {
            UpdateTruckImage.sprite = UpdateTruckSprites[0];
            UpdatePanel.SetActive(false);
        }
        else if (UpdateTruckImage.sprite == UpdateTruckSprites[3])
        {
            UpdateTruckImage.sprite = UpdateTruckSprites[1];
            UpdatePanel.SetActive(false);
        }
    }

    public void OpenBoostPopup()
    {
        UpdatePanel.SetActive(false);

        if (BoostTruckImage.sprite == BoostTruckSprites[0])
        {
            BoostTruckImage.sprite = BoostTruckSprites[2];
            BoostPanel.SetActive(true);
        }
        else if (BoostTruckImage.sprite == BoostTruckSprites[1])
        {
            BoostTruckImage.sprite = BoostTruckSprites[3];
            BoostPanel.SetActive(true);
        }
        else if (BoostTruckImage.sprite == BoostTruckSprites[2])
        {
            BoostTruckImage.sprite = BoostTruckSprites[0];
            BoostPanel.SetActive(false);
        }
        else if (BoostTruckImage.sprite == BoostTruckSprites[3])
        {
            BoostTruckImage.sprite = BoostTruckSprites[1];
            BoostPanel.SetActive(false);
        }
    }
}
