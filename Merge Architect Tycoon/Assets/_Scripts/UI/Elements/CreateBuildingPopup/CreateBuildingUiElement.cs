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

    public void SetBuildingName(string newName)
    {
        buildingName = newName;
    }
}