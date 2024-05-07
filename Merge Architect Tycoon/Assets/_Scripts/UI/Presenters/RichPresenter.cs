using UniRx;
using Zenject;

public class RichPresenter
{
    [Inject] private PlayerProgressService _progressService;
    [Inject] private PlayerProgress _progress;
    [Inject] private RichPanel _richPanel;

    public void OnSceneLoaded()
    {
        _richPanel.PlusCoinButton.onClick.AddListener(AddCoins);
        _richPanel.PlusDiamondButton.onClick.AddListener(AddDiamonds);

        _progress.Riches.Coins.Subscribe(i => _richPanel.RenderMoneyCount(i));
        _progress.Riches.Diamonds.Subscribe(i => _richPanel.RenderDiamandCount(i));

        _richPanel.RenderMoneyCount(_progress.Riches.Coins.Value);
        _richPanel.RenderDiamandCount(_progress.Riches.Diamonds.Value);
    }

    public void AddCoins()
    {
        _progressService.AddCoins(1000);

// #if UNITY_EDITOR
//         if (Application.version.StartsWith("d"))
//         {
//             _progressService.AddCoins(1000);
//         }
//         else
//         {
// #endif
//             //Shop.TryToBuy();
// #if UNITY_EDITOR
//         }
// #endif
    }

    public void AddDiamonds()
    {
        _progressService.AddDiamonds(100);

//         _progressService.AddDiamonds(100);
// #if UNITY_EDITOR
//         if (Application.version.StartsWith("d"))
//         {
//             _progressService.AddDiamonds(100);
//         }
// #endif
    }
}