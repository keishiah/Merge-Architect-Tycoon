using CodeBase.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class CoinsCounterUi : UiViewBase
    {
        public TextMeshProUGUI moneyCountText;
        public Button addCoinsButton;

        private UiPresenter _uiPresenter;

        [Inject]
        void Construct(UiPresenter uiPresenter)
        {
            _uiPresenter = uiPresenter;
            InitUiElement(_uiPresenter);
        }

        public override void InitUiElement(UiPresenter uiPresenter)
        {
            Coins coins = uiPresenter.PlayerProgressService.Progress.Coins;
            int coinsCurrentCoinsCount = coins.CurrentCoinsCount;
            RenderCoinsCount(coinsCurrentCoinsCount);

            _uiPresenter = uiPresenter;

            uiPresenter.SubscribeMoneyCountChanged(RenderCoinsCount);
            addCoinsButton.onClick.AddListener(AddCoins);
        }

        private void AddCoins()
        {
            _uiPresenter.PlayerProgressService.Progress.Coins.AddCoins(5);
        }

        private void RenderCoinsCount(int newValue)
        {
            moneyCountText.text = newValue.ToString();
        }
    }
}