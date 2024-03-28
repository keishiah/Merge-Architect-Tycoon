using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TruckPanel : MonoBehaviour
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
    private TextMeshProUGUI _updateTruckButtonText;
    [SerializeField]
    private Button _boostTruckButton;
    [SerializeField]
    private TextMeshProUGUI[] _boostText;
    [SerializeField]
    private Slider[] _boostSlider;
    [SerializeField]
    private Button[] _resourceButtons;

    private int _boostLimit;

    public void BoostInit(int limit)
    {
        _boostLimit = limit;
        foreach (Slider slider in _boostSlider)
        {
            slider.maxValue = _boostLimit;
        }
    }

    public void RefreshBoostText(int count)
    {
        foreach (var item in _boostText)
        {
            item.text = $"{count}/{_boostLimit}";
        }
    }

    public void UpdateButtonRefresh(string text, bool interactable = true)
    {
        _updateTruckButtonText.text = text;
        _updateTruckButton.interactable = interactable;
    }

    public void BoostButtonRefresh()
    {
        throw new NotImplementedException();
    }

    public void ResourcesRefresh(int level)
    {
        for(int i = 0; i < _resourceButtons.Length; i++)
        {
            _resourceButtons[0].gameObject.SetActive(i<level);
        }
    }
}
