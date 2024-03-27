using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RichPanel : MonoBehaviour
{
    public TextMeshProUGUI moneyCountText;
    public TextMeshProUGUI diamondCountText;

    public Button PlusCoinButton;
    public Button PlusDiamondButton;

    public void RenderMoneyCount(int newValue)
    {
        moneyCountText.text = newValue.ToString();
    }

    public void RenderDiamandCount(int newValue)
    {
        diamondCountText.text = newValue.ToString();
    } 
}
