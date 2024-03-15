using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DistrictUi : MonoBehaviour
{
    public int districtId;
    public Slider coinsSlider;
    public Button earnCurrencyButton;
    public Button closeMapButton;

    public CancellationTokenSource ActivityToken { get; private set; }

    private DistrictsPresenter _districtsPresenter;

    [Inject]
    void Construct(DistrictsPresenter districtsPresenter)
    {
        _districtsPresenter = districtsPresenter;

        Initialize();
    }

    public void SetSliderMaxValue(float maxValue) => coinsSlider.maxValue = maxValue;
    public void SetSliderValue(float value) => coinsSlider.value = value;

    private void Initialize()
    {
        ActivityToken = new CancellationTokenSource();

        _districtsPresenter.AddDistrict(this);

        earnCurrencyButton.onClick.AddListener(EarnCurrency);
        closeMapButton.onClick.AddListener(CloseMap);

        coinsSlider.gameObject.SetActive(false);
        earnCurrencyButton.gameObject.SetActive(false);
    }

    private void EarnCurrency()
    {
        _districtsPresenter.EarnCurrency(districtId);
        earnCurrencyButton.gameObject.SetActive(false);
    }

    public void OpenDistrict()
    {
        _districtsPresenter.SetCurrentDistrict(districtId);
    }

    private void CloseMap()
    {
        OpenDistrict();
        _districtsPresenter.CloseMap();
    }

    public void TurnOnEarnButton()
    {
        earnCurrencyButton.gameObject.SetActive(true);
    }

    public void OnDestroy()
    {
        ActivityToken?.Cancel();
        ActivityToken?.Dispose();
    }
}