using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Services;
using CodeBase.UI.Elements;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class UiPresenter
    {
        public IPlayerProgressService PlayerProgressService;
        public IStaticDataService _staticDataService;

        private List<UiViewBase> _uiElements = new();

        [Inject]
        void Construct(IPlayerProgressService playerProgressService, IStaticDataService staticDataService)
        {
            PlayerProgressService = playerProgressService;
            _staticDataService = staticDataService;
        }

        public void OpenCreateBuildingPopup()
        {
            CreateBuildingPopup popup = GetUiElementFromElementsList<CreateBuildingPopup>();
            popup.gameObject.SetActive(true);
            popup.InitPopupElements();
        }

        public void CloseCreateBuildingPopup() =>
            GetUiElementFromElementsList<CreateBuildingPopup>().gameObject.SetActive(false);

        public void SubscribeMoneyCountChanged(Action<int> actionOnCoinsCountChanged)
        {
            PlayerProgressService.Progress.Coins.SubscribeToCoinsCountChanges(actionOnCoinsCountChanged);
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