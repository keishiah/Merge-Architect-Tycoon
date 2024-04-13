using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DistrictPopup : MonoBehaviour
{
    public int DistrictId;
    public Slider CoinsSlider;
    public Button DistrictButton;
    public Image DollarsImage;

    public CancellationTokenSource ActivityToken { get; private set; }

    private DistrictsPresenter _districtsPresenter;

    [Inject]
    void Construct(DistrictsPresenter districtsPresenter)
    {
        _districtsPresenter = districtsPresenter;

        Initialize();
    }

    public void SetSliderMaxValue(float maxValue) => CoinsSlider.maxValue = maxValue;
    public void SetSliderValue(float value) => CoinsSlider.value = value;

    private void Initialize()
    {
        ActivityToken = new CancellationTokenSource();

        _districtsPresenter.AddDistrict(this);

        DistrictButton.onClick.AddListener(TapDistrict);
        DistrictButton.GetComponent<Image>().alphaHitTestMinimumThreshold = 1f;
        CoinsSlider.gameObject.SetActive(false);
        DollarsImage.enabled = false;
    }

    private void TapDistrict()
    {
        if (DollarsImage.enabled)
        {
            _districtsPresenter.EarnCurrency(DistrictId);
            DollarsImage.enabled = false;
        }
        else
            CloseMap();
    }

    public void OpenDistrict()
    {
        _districtsPresenter.SetCurrentDistrict(DistrictId);
    }

    private void CloseMap()
    {
        OpenDistrict();
        _districtsPresenter.CloseMap();
    }

    public void TurnOnEarnButton()
    {
        DollarsImage.enabled = true;
    }

    public void OnDestroy()
    {
        ActivityToken?.Cancel();
        ActivityToken?.Dispose();
    }
}