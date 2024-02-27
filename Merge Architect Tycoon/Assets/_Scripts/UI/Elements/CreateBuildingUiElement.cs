using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class CreateBuildingUiElement : MonoBehaviour
    {
        public Button buildingButton;
        public Image buildingImage;
        public Image coinsImage;
        public Image resourceImage;

        public TextMeshProUGUI coinsPriceTex;
        public TextMeshProUGUI resourcePriceTex;

        public Outline buildingImageOutline;
        public string buildingName;


        private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

        public void SetPresenter(CreateBuildingPopupPresenter presenter)
        {
            _createBuildingPopupPresenter = presenter;
            buildingButton.onClick.AddListener(() => _createBuildingPopupPresenter.SelectBuilding(this));
        }


        public void SetBuildingImage(Sprite buildingSprite) => buildingImage.sprite = buildingSprite;
        public void SetResourceImage(Sprite buildingSprite) => resourceImage.sprite = buildingSprite;
        public void SetCoinsPriceText(string text) => coinsPriceTex.text = text;
        public void SetResourcesPriceText(string text) => resourcePriceTex.text = text;

        public void SetBuildingName(string newName)
        {
            buildingName = newName;
        }
    }
}