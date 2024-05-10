using System;
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
    public TextMeshProUGUI UpdateTruckBySoftText;
    public Button UpdateTruckByHardButton;
    public TextMeshProUGUI UpdateTruckByHardText;

    public TextMeshProUGUI UpdateTruckHeaderText;
    public Image UpdateTruckImage;

    private bool isEnoughSoftToUpdate;
    private bool isEnoughHardToUpdate;
    [Header("Boost")]
    public Sprite[] BoostTruckSprites;
    public GameObject BoostPanel;
    public Button BoostTruckButton;
    public TextMeshProUGUI BoostTruckButtonText;
    public TextMeshProUGUI BoostTexts;
    public Image BoostTruckImage;

    [Header("Else")]
    public GameObject ToShopPopup;
    public GameObject ClosePopupsTruckButton;

    public void RenderBoost(int count)
    {
        BoostTexts.text = $"{count}";
    }
    public void RenderResourceCost(int price)
    {
        PriceText.text = $"-{price}";
    }

    public void UpdateButtonRefresh(string text, TruckUpdates update = null)
    {
        UpdateTruckHeaderText.text = text;

        if (update != null)
        {
            UpdateTruckBySoftText.text = update.SoftCost.ToString();
            UpdateTruckByHardText.text = update.HardCost.ToString();
        }
        else
        {
            UpdateTruckBySoftText.text = "inf";
            UpdateTruckByHardText.text = "inf";
        }
    }

    public void BoostButtonRefresh(int i)
    {
        BoostTruckButtonText.text = $"{i}";
    }

    public void InteracteblesBySoft(bool isSoftUpdatable, bool isBuyble)
    {
        RebuyTruckImage.sprite = isBuyble ? RebuyTruckSprites[0] : RebuyTruckSprites[1];

        isEnoughSoftToUpdate = isSoftUpdatable;
        CheckUpdateSprite();
    }
    public void InteracteblesByHard(bool isHardUpdatable, bool isBoostable)
    {
        bool isBoostSelected = BoostPanel.activeSelf;
        if (!isBoostSelected)
            BoostTruckImage.sprite = isBoostable ? BoostTruckSprites[0] : BoostTruckSprites[1];
        else
            BoostTruckImage.sprite = isBoostable ? BoostTruckSprites[2] : BoostTruckSprites[3];

        isEnoughHardToUpdate = isHardUpdatable;
        CheckUpdateSprite();
    }

    private void CheckUpdateSprite()
    {
        bool isUpdatable = isEnoughSoftToUpdate || isEnoughHardToUpdate;
        bool isSelected = UpdatePanel.activeSelf;
        if (!isSelected)
            UpdateTruckImage.sprite = isUpdatable ? UpdateTruckSprites[0] : UpdateTruckSprites[1];
        else
            UpdateTruckImage.sprite = isUpdatable ? UpdateTruckSprites[2] : UpdateTruckSprites[3];
    }

    public void BuyTruckButtonsAddListener(UnityAction buyTruck)
    {
        BuyTruckButtons.onClick.AddListener(buyTruck);
    }

    public void OpenUpdatePopup()
    {
        CloseBoost();

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

        ClosePopupsTruckButton.SetActive(UpdatePanel.activeSelf);
    }

    private void CloseBoost()
    {
        BoostPanel.SetActive(false);
        if (BoostTruckImage.sprite == BoostTruckSprites[2])
            BoostTruckImage.sprite = BoostTruckSprites[0];
        else if (BoostTruckImage.sprite == BoostTruckSprites[3])
            BoostTruckImage.sprite = BoostTruckSprites[1];
    }

    public void OpenBoostPopup()
    {
        CloseUpdate();

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

        ClosePopupsTruckButton.SetActive(BoostPanel.activeSelf);
    }

    private void CloseUpdate()
    {
        UpdatePanel.SetActive(false);

        if (UpdateTruckImage.sprite == UpdateTruckSprites[2])
            UpdateTruckImage.sprite = UpdateTruckSprites[0];
        else if (UpdateTruckImage.sprite == UpdateTruckSprites[3])
            UpdateTruckImage.sprite = UpdateTruckSprites[1];

        
    }

    public void ClosePopups()
    {
        CloseBoost();
        CloseUpdate();
        ClosePopupsTruckButton.SetActive(false);
    }

    public void ShowToShopPopup()
    {
        ToShopPopup.SetActive(true);
    }
}
