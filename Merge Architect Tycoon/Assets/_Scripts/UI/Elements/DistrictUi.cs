using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DistrictUi : MonoBehaviour
{
    public int districtId;
    public Slider coinsSlider;
    public Button openDistrictOne;
    public Button earnCurrencyButton;
    public District districtOne;

    public CancellationTokenSource ActivityToken;

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
        openDistrictOne.onClick.AddListener(OpenDistrict);
        earnCurrencyButton.onClick.AddListener(EarnCurrency);

        coinsSlider.gameObject.SetActive(false);
        earnCurrencyButton.gameObject.SetActive(false);
    }

    private void EarnCurrency()
    {
        _districtsPresenter.EarnCurrency(districtId);
        earnCurrencyButton.gameObject.SetActive(false);

    }

    private void OpenDistrict()
    {
        districtOne.gameObject.SetActive(true);
        _districtsPresenter.CitiesMapPopup.SetActive(false);
        _districtsPresenter.SetCurrentDistrict(1);
    }

    public void TurnOnEarnButton()
    {
        earnCurrencyButton.gameObject.SetActive(true);
    }
    public void OnDestroy()
    {
        ActivityToken?.Cancel();
        ActivityToken?.Dispose();
        ActivityToken = null;
    }
}