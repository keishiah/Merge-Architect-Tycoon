using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Services.PlayerProgressService;
using _Scripts.Services.StaticDataService;
using _Scripts.UI.Elements;
using Zenject;

namespace _Scripts.UI.Presenters
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

        public void AddUiElementToElementsList(UiViewBase element)
        {
            _uiElements.Add(element);
        }

        public void SubscribeMoneyCountChanged(Action<int> actionOnCoinsCountChanged)
        {
            PlayerProgressService.Progress.Coins.SubscribeToCoinsCountChanges(actionOnCoinsCountChanged);
        }

        public void SubscribeDiamondsCountChanged(Action<int> actionOnDiamondsCountChanged)
        {
            PlayerProgressService.Progress.Diamonds.SubscribeToCoinsCountChanges(actionOnDiamondsCountChanged);
        }

        public void InitializeElementsOnSceneLoaded()
        {
            foreach (var element in _uiElements)
            {
                if (element.GetComponent<IInitializableOnSceneLoaded>() != null)
                    element.GetComponent<IInitializableOnSceneLoaded>().OnSceneLoaded();
            }
        }

        private T GetUiElementFromElementsList<T>() where T : UiViewBase
        {
            return (T)_uiElements.FirstOrDefault(element => element.GetType() == typeof(T));
        }
    }
}