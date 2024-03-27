using TMPro;
using UnityEngine;
using UniRx;
using Zenject;

public class RichPanel : MonoBehaviour
{
    public TextMeshProUGUI moneyCountText;
    public TextMeshProUGUI diamandCountText;

    [Inject] private PlayerProgressService _progressService;
    [Inject] private PlayerProgress _progress;

    private void Awake()
    {
        _progress.Riches.Coins.Subscribe(i => RenderMoneyCount(i));
        _progress.Riches.Diamonds.Subscribe(i => RenderDiamandCount(i));
        
        RenderMoneyCount(_progress.Riches.Coins.Value);
        RenderDiamandCount(_progress.Riches.Diamonds.Value);
    }

    public void AddCoins()
    {
// #if UNITY_EDITOR
        if (Application.version.StartsWith("d"))
        {
            _progressService.AddCoins(100500);
        }
        // else
        // {
// #endif
        //Shop.TryToBuy();
// #if UNITY_EDITOR
//             }
// #endif
    }

    public void AddDiamonds()
    {
        if (Application.version.StartsWith("d"))
        {
            _progressService.AddDiamonds(100500);
        }
    }

    public void RenderMoneyCount(int newValue)
    {
        moneyCountText.text = newValue.ToString();
    }

    public void RenderDiamandCount(int newValue)
    {
        diamandCountText.text = newValue.ToString();
    }
}