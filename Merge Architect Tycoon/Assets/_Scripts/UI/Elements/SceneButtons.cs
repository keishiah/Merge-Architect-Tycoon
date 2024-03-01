using _Scripts.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI.Elements
{
    public class SceneButtons : MonoBehaviour
    {
        public Button buildingConstructMenuButton;
        public Button mergeMenuButton;
        public Button closeButton;
        public Button goToMapButton;

        public GameObject mergePanel;
        public GameObject citiesMapPopup;

        private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

        [Inject]
        void Construct(CreateBuildingPopupPresenter createBuildingPopupPresenter)
        {
            _createBuildingPopupPresenter = createBuildingPopupPresenter;
        }

        private void Start()
        {
            buildingConstructMenuButton.onClick.AddListener(OpenBuildingConstructMenu);

            mergeMenuButton.onClick.AddListener(OpenMergeMenuButton);
            closeButton.onClick.AddListener(CloseElement);
            goToMapButton.onClick.AddListener(OpenCitiesMap);

            closeButton.gameObject.SetActive(false);
        }

        private void OpenCitiesMap()
        {
            citiesMapPopup.gameObject.SetActive(true);
        }

        private void CloseElement()
        {
            mergePanel.gameObject.SetActive(false);
            _createBuildingPopupPresenter.CLoseScroller();
            gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
        }

        private void OpenBuildingConstructMenu()
        {
            gameObject.SetActive(false);
            _createBuildingPopupPresenter.OpenScroller();
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