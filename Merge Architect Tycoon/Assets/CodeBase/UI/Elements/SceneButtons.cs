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
            closeButton.gameObject.SetActive(false);
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