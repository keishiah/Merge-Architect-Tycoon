using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateBuildingUiElement : MonoBehaviour
{
    public Button buildingButton;
    public Image buildingImage;
    public TextMeshProUGUI coinsPriceTex;
    public List<Image> resourceElements;

    public Outline buildingImageOutline;
    public string buildingName;


    private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

    public void SetPresenter(CreateBuildingPopupPresenter presenter)
    {
        _createBuildingPopupPresenter = presenter;
        buildingButton.onClick.AddListener(() => _createBuildingPopupPresenter.SelectBuilding(this));
    }

    public void SetBuildingImage(Sprite buildingSprite) => buildingImage.sprite = buildingSprite;
    public void SetCoinsPriceText(string text) => coinsPriceTex.text = text;
    public void SetResourcesImages(List<MergeItem> items)
    {
        HideAllResources();
        for (var i = 0; i < items.Count; i++)
        {
            resourceElements[i].gameObject.SetActive(true);
            resourceElements[i].sprite = items[i].ItemSprite;
        }
    }

    private void HideAllResources()
    {
        foreach (var resourceElement in resourceElements)
        {
            resourceElement.gameObject.SetActive(false);
        }
    }

    public void SetBuildingName(string newName)
    {
        buildingName = newName;
    }
}