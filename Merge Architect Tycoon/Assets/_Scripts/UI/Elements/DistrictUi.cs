using System.Threading;
using _Scripts.Logic.CityData;
using _Scripts.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI.Elements
{
    public class DistrictUi : MonoBehaviour
    {
        public int districtId;
        public Slider coinsSlider;
        public Button openDistrictOne;
        public District districtOne;
        public CancellationTokenSource ActivityToken;

        private DistrictsPresenter _districtsPresenter;

        [Inject]
        void Construct(DistrictsPresenter districtsPresenter)
        {
            _districtsPresenter = districtsPresenter;
            Initialize();
            ActivityToken = new CancellationTokenSource();
        }

        public void SetSliderMaxValue(float maxValue) => coinsSlider.maxValue = maxValue;
        public void SetSliderValue(float value) => coinsSlider.value = value;

        private void Initialize()
        {
            _districtsPresenter.AddDistrict(this);
            openDistrictOne.onClick.AddListener(OpenDistrict);
        }

        private void OpenDistrict()
        {
            districtOne.gameObject.SetActive(true);
            _districtsPresenter.CitiesMapPopup.SetActive(false);
            _districtsPresenter.SetCurrentDistrict(1);
        }

        public void OnDestroy()
        {
            ActivityToken?.Cancel();
            ActivityToken?.Dispose();
            ActivityToken = null;
        }
    }
}