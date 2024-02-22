using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Services;
using CodeBase.UI.Elements;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class UiPresenter
    {
        public IPlayerProgressService PlayerProgressService;
        public IStaticDataService StaticDataService;
        private List<UiViewBase> _uiElements = new();

        [Inject]
        void Construct(IPlayerProgressService playerProgressService, IStaticDataService staticDataService)
        {
            PlayerProgressService = playerProgressService;
            StaticDataService = staticDataService;
        }

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