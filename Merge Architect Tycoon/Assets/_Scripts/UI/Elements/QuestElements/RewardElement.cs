using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class RewardElement : MonoBehaviour
    {
        public TextMeshProUGUI questText;
        public Image rewardImage1;
        
        public void RenderReward(string text, Sprite sprite)
        {
            questText.text = text;
            rewardImage1.sprite = sprite;
        }
    }
}