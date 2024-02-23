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
        public Button addCoinsButton;

        private UiPresenter _uiPresenter;

        [Inject]
        void Construct(UiPresenter uiPresenter)
        {
            _uiPresenter = uiPresenter;
        }

        private void Awake()
        {
            InitUiElement(_uiPresenter);
        }

        public override void InitUiElement(UiPresenter uiPresenter)
        {
            IPlayerProgressService a = uiPresenter.PlayerProgressService;
            Progress b = a.Progress;
            Coins c = b.Coins;
            int currentMoneyCount = c.CurrentCoinsCount;
            RenderMoneyCount(currentMoneyCount);

            _uiPresenter = uiPresenter;

            uiPresenter.SubscribeMoneyCountChanged(RenderMoneyCount);
            addCoinsButton.onClick.AddListener(AddCoins);
        }

        private void AddCoins()
        {
            _uiPresenter.PlayerProgressService.Progress.Coins.AddCoins(5);
        }

        private void RenderMoneyCount(int newValue)
        {
            moneyCountText.text = newValue.ToString();
        }
    }
}