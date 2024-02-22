using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Build_0._0._0
{
    public class EnableCreateBuildingPopup : MonoBehaviour
    {
        public Button enablePopup;
        private UiPresenter _uiPresenter;
        private bool isOpened = false;

        [Inject]
        void Construct(UiPresenter uiPresenter)
        {
            _uiPresenter = uiPresenter;
        }

        private void Start()
        {
            enablePopup.onClick.AddListener(OpenClosePopup);
        }

        private void OpenClosePopup()
        {
            if (!isOpened)
            {
                _uiPresenter.OpenCreateBuildingPopup();
                isOpened = true;
            }
            else
            {
                _uiPresenter.CloseCreateBuildingPopup();
                isOpened = false;
            }
        }
    }
}