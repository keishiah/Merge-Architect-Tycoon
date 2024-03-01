using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Elements
{
    public class DistrictUi : MonoBehaviour
    {
        public Slider coinsSlider;
        
        public void SetSliderMaxValue(int maxValue) => coinsSlider.maxValue = maxValue;
        public void SetSliderValue(int value) => coinsSlider.value = value;

    }
}