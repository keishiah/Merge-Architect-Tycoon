using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Buildings;
using CodeBase.Services.PlayerProgressService;
using CodeBase.UI.Elements;
using Zenject;

namespace CodeBase.UI
{
    public class UiPresenter
    {
        private IPlayerProgressService _playerProgressService;
        public IPlayerProgressService PlayerProgressService => _playerProgressService;

        private List<UiViewBase> _uiElements = new();

        [Inject]
        void Construct(IPlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
        }

        public void OpenCreateBuildingPopup(BuildingPlace buildingPlace)
        {
            CreateBuildingPopup popup = GetUiElementFromElementsList<CreateBuildingPopup>();
            popup.SubscribeToCreateBuilding();
            popup.gameObject.SetActive(true);
        }

        public void OpenCreateBuildingPopup()
        {
            CreateBuildingPopup popup = GetUiElementFromElementsList<CreateBuildingPopup>();
            popup.SubscribeToCreateBuilding();
            popup.gameObject.SetActive(true);
        }

        public void CloseCreateBuildingPopup() =>
            GetUiElementFromElementsList<CreateBuildingPopup>().gameObject.SetActive(false);

        public void SubscribeMoneyCountChanged(Action<int> actionOnCoinsCountChanged)
        {
            PlayerProgressService.Progress.Coins.SubscribeToCoinsCountChanges(actionOnCoinsCountChanged);
        }

        public void SubscribeUIElementToPresenter(UiViewBase uiElement)
        {
            uiElement.InitUiElement(this);
        }

        public void AddUiElementToElementsList(UiViewBase element)
        {
            _uiElements.Add(element);
        }

        private T GetUiElementFromElementsList<T>() where T : UiViewBase
        {
            return (T)_uiElements.FirstOrDefault(element => element.GetType() == typeof(T));
        }
    }
}