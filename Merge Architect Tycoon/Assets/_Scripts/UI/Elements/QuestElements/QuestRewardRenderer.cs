using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestRewardRenderer : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public Image rewardImage;
        
    public void RenderReward(string text, Sprite sprite)
    {
        questText.text = text;
        rewardImage.sprite = sprite;
    }
}