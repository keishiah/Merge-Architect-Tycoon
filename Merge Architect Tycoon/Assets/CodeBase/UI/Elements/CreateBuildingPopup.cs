using CodeBase.Logic.Buildings;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class CreateBuildingPopup : UiViewBase
    {
        public Button buildButton;
        private UiPresenter _uiPresenter;

        public override void InitUiElement(UiPresenter uiPresenter)
        {
            uiPresenter.AddUiElementToElementsList(this);
            gameObject.SetActive(false);
            _uiPresenter = uiPresenter;
            _uiPresenter.PlayerProgressService.Progress.Coins._coinsCount.Subscribe(newValue =>
                MakeButtonInteractable(newValue));
        }

        public void SubscribeToCreateBuilding()
        {
            buildButton.onClick.AddListener(() =>
            {
                if (_uiPresenter.PlayerProgressService.Progress.Coins.CurrentCoinsCount < 10) return;
                GameObject.Find("BuildingPlace").GetComponent<BuildingPlace>().StartCreatingBuilding("Name");
                _uiPresenter.PlayerProgressService.Progress.Coins.SpendCoins(10);

                // buildingPlace.StartCreatingBuilding("Name");
                gameObject.SetActive(false);
            });
        }

        private void MakeButtonInteractable(int coinsvalue)
        {
            buildButton.interactable = coinsvalue >= 10;
        }

        private void OnDisable()
        {
            buildButton.onClick.RemoveAllListeners();
        }
    }
}