using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TruckPresenter : MonoBehaviour
{
    [SerializeField]
    private Button _menuTruckButton;
    [SerializeField]
    private Sprite[] _rebuyTruckSprites;
    [SerializeField]
    private Image _rebuyTruckImage;
    [SerializeField]
    private Button _updateTruckButton;
    [SerializeField]
    private Button _boostTruckButton;
    [SerializeField]
    private TextMeshProUGUI[] _boostText;
    [SerializeField]
    private Slider[] _boostSlider;
    [SerializeField]
    private Button[] _resourceButtons;

    [Inject] private StaticDataService _staticDataService;
    [Inject] private PlayerProgress _playerProgress;

    private int _boostLimit;

    private void Start()
    {
        BoostInit();
    }

    private void BoostInit()
    {
        // _boostLimit = _staticDataService.TruckInfo.BoostLimit;
        _boostLimit = 3;
    }

    private void RefreshBoostText(int count)
    {
        foreach (var item in _boostText)
        {
            item.text = $"{count}/{_boostLimit}";
        }
    }

    private void UpdateButtonRefresh(int level)
    {
        if(level >= _staticDataService.TruckInfo.TruckUpdates.Length)
        {
            _updateTruckButton.GetComponent<TextMeshProUGUI>().text = "MAX LEVEL";
            _updateTruckButton.interactable = false;
            return;
        }

        var nextUpdate = _staticDataService.TruckInfo.TruckUpdates[level];
        _updateTruckButton.GetComponent<TextMeshProUGUI>().text = $"+{nextUpdate.TruskUpdate} \n {nextUpdate.SoftCost}$";
    }

    private void BoostButtonRefresh()
    {
        throw new NotImplementedException();
    }

    private void ResourcesRefresh(int level)
    {
        for(int i = 0; i < _resourceButtons.Length; i++)
        {
            _resourceButtons[0].gameObject.SetActive(i<level);
        }
    }
}
