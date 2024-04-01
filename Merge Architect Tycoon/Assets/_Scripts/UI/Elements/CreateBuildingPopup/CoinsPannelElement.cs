using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CoinsPannelElement : MonoBehaviour
{
    public TextMeshProUGUI resourceName;
    public TextMeshProUGUI resourceCount;
    public Image checkMark;

    public void RenderCoinsElement(int currentCount, int resourceGoal)
    {
        resourceName.text = "Coins";
        RenderCoinsCount(currentCount, resourceGoal);
    }


    public void RenderCoinsCount(int currentCoins, int coinsGoal)
    {
        if (currentCoins >= coinsGoal)
        {
            resourceCount.text = $"{coinsGoal}/{coinsGoal}";
            checkMark.gameObject.SetActive(true);
        }
        else
        {
            resourceCount.text = $"{currentCoins}/{coinsGoal}";
            checkMark.gameObject.SetActive(false);
        }
    }
}