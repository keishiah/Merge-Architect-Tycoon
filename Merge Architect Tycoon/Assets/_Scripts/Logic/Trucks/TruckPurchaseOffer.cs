using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TruckPurchaseOffer : MonoBehaviour
{
    [Inject]
    private TruckPresenter _truckPresenter;
    [Inject]
    private IPlayerProgressService _playerProgressService;
    [Inject]
    private InformationPanel _informationPanel;
    [SerializeField]
    private GameObject _truckMenu;
    [SerializeField]
    private string _title;
    [SerializeField]
    private Truck _truck;
    [SerializeField]
    private int _cost;

    [SerializeField]
    private TextMeshProUGUI _titleText;
    [SerializeField]
    private Button _buyButton;
    [SerializeField]
    private TextMeshProUGUI _costText;
    [SerializeField]
    private Button _viewBuyButton;

    [SerializeField]
    private Image _truckImage;
    [SerializeField]
    private Image _resourceImage;

    void Start()
    {
        if (_truckMenu == null)
            throw new Exception("Truck menu is null!");
        if (String.IsNullOrEmpty(_title))
            throw new Exception("No TITLE!");
        if (_truck == null)
            throw new Exception("The truck data is not filled in!");
        if (_truck.SpriteImage == null)
            throw new Exception("The truck sprite is not filled in!");
        if (_truck.TruckCargo.Count == 0)
            throw new Exception("The truck cargo is not filled in!");
        if (_truck.TruckCargo[0].itemSprite == null)
            throw new Exception("The truck cargo sprite is not filled in!");

        InitializeImages();
        InitializeText();
        _buyButton.onClick.AddListener(AddNewTruck);
    }

    private void InitializeText()
    {
        _titleText.text = _title;
        _costText.text = _cost.ToString();
    }

    private void InitializeImages()
    {
        _truckImage.sprite = _truck.SpriteImage;
        _resourceImage.sprite = _truck.TruckCargo[0].itemSprite;
    }

    private void AddNewTruck()
    {
        if(_playerProgressService.Progress.Coins.SpendCoins(_cost))
            _truckPresenter.AddNewTruck(_truck.Clone());
        _truckMenu.SetActive(false);
        _informationPanel.gameObject.SetActive(true);
    }
}
