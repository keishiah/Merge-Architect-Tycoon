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
        resourceCount.transform.parent.gameObject.SetActive(true);

        if (currentCoins >= coinsGoal)
        {
            // resourceCount.text = $"{coinsGoal}/{coinsGoal}";
            resourceCount.transform.parent.gameObject.SetActive(false);
            checkMark.gameObject.SetActive(true);
        }
        else
        {
            resourceCount.text = $"{currentCoins}/{coinsGoal}";
            checkMark.gameObject.SetActive(false);
        }
    }
}