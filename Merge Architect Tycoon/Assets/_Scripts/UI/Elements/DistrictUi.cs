using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DistrictUi : MonoBehaviour
{
    public int districtId;
    public Slider coinsSlider;
    public Button earnCurrencyButton;

    public CancellationTokenSource ActivityToken { get; set; }

    private DistrictsPresenter _districtsPresenter;
    private SceneButtons _sceneButtons;

    [Inject]
    void Construct(DistrictsPresenter districtsPresenter, SceneButtons sceneButtons)
    {
        _districtsPresenter = districtsPresenter;
        _sceneButtons = sceneButtons;

        Initialize();
    }

    public void SetSliderMaxValue(float maxValue) => coinsSlider.maxValue = maxValue;
    public void SetSliderValue(float value) => coinsSlider.value = value;

    private void Initialize()
    {
        ActivityToken = new CancellationTokenSource();

        _districtsPresenter.AddDistrict(this);
        earnCurrencyButton.onClick.AddListener(EarnCurrency);

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
        _districtsPresenter.SetCurrentDistrict(1);
        _sceneButtons.CloseCurrentWidget();
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