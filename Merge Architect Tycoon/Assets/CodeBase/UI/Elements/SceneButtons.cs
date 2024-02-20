using System;
using CodeBase.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class SceneButtons : MonoBehaviour
    {
        public Button buildingConstructMenuButton;
        public Button mergeMenuButton;
        public Button closeButton;

        public GameObject mergePanel;
        public GameObject buildingConstructMenu;
        
        private UiPresenter _uiPresenter;

        [Inject]
        void Construct(UiPresenter uiPresenter)
        {
            _uiPresenter = uiPresenter;
        }
        private void Start()
        {
            buildingConstructMenuButton.onClick.AddListener(OpenBuildingConstructMenu);
            mergeMenuButton.onClick.AddListener(OpenMergeMenuButton);
            closeButton.onClick.AddListener(CloseElement);
            closeButton.gameObject.SetActive(false);
        }

        private void CloseElement()
        {
            mergePanel.gameObject.SetActive(false);
            buildingConstructMenu.gameObject.SetActive(false);
            gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
        }

        private void OpenBuildingConstructMenu()
        {
            gameObject.SetActive(false);
            _uiPresenter.OpenCreateBuildingPopup();
            closeButton.gameObject.SetActive(true);
        }

        private void OpenMergeMenuButton()
        {
            gameObject.SetActive(false);
            mergePanel.SetActive(true);
            closeButton.gameObject.SetActive(true);
        }
    }
}