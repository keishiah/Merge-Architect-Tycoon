using CodeBase.Data;
using CodeBase.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class CoinsCounterUi : UiViewBase
    {
        public TextMeshProUGUI moneyCountText;
        public TextMeshProUGUI diamandCountText;

        private UiPresenter _uiPresenter;
        private IPlayerProgressService _playerProgressService;

        [Inject]
        void Construct(UiPresenter uiPresenter, IPlayerProgressService playerProgressService)
        {
            _uiPresenter = uiPresenter;
            _playerProgressService = playerProgressService;
        }

        private void Awake()
        {
            InitUiElement(_uiPresenter);
        }

        public override void InitUiElement(UiPresenter uiPresenter)
        {
            IPlayerProgressService a = uiPresenter.PlayerProgressService;
            Progress progress = a.Progress;
            Coins coins = progress.Coins;
            int currentMoneyCount = coins.CurrentCoinsCount;
            RenderMoneyCount(currentMoneyCount);

            _uiPresenter = uiPresenter;

            progress.Coins.SubscribeToCoinsCountChanges(RenderMoneyCount);
            progress.Diamands.SubscribeToCoinsCountChanges(RenderDiamandCount);
        }

        public void AddCoins()
        {
#if UNITY_EDITOR
            if (Application.version.StartsWith("d"))
            {
                _playerProgressService.Progress.AddCoins(100500);
                _playerProgressService.Progress.AddDiamonds(100500);
            }
            else
            {
#endif
                //Shop.TryToBuy();
#if UNITY_EDITOR
            }
#endif
        }

        private void RenderMoneyCount(int newValue)
        {
            moneyCountText.text = newValue.ToString();
        }

        private void RenderDiamandCount(int newValue)
        {
            diamandCountText.text = newValue.ToString();
        }
    }
}