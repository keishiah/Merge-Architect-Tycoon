using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Buildings;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.StaticDataService;
using CodeBase.UI.Elements;
using Zenject;

namespace CodeBase.UI
{
    public class UiPresenter
    {
        public IPlayerProgressService PlayerProgressService;
        public CreateBuildingPopupPresenter _createBuildingPopupPresenter;
        public IStaticDataService _staticDataService;

        private List<UiViewBase> _uiElements = new();


        [Inject]
        void Construct(IPlayerProgressService playerProgressService,
            CreateBuildingPopupPresenter createBuildingPopupPresenter, IStaticDataService staticDataService)
        {
            PlayerProgressService = playerProgressService;
            _createBuildingPopupPresenter = createBuildingPopupPresenter;
            _staticDataService = staticDataService;
        }

        public void OpenCreateBuildingPopup()
        {
            CreateBuildingPopup popup = GetUiElementFromElementsList<CreateBuildingPopup>();
            popup.InitPopupButtons();
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