using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class CreateBuildingPopup : MonoBehaviour
    {
        public Button goToMergePanelButton;
        public Button createBuildingButton;

        public GameObject mergePanel;
        public Button closeButton;


        private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

        [Inject]
        void Construct(CreateBuildingPopupPresenter createBuildingPopupPresenter)
        {
            _createBuildingPopupPresenter = createBuildingPopupPresenter;
            _createBuildingPopupPresenter.InitializePopup(this);
            ClosePanelButtons();

            goToMergePanelButton.onClick.AddListener(GoToMergePanel);
            createBuildingButton.onClick.AddListener(_createBuildingPopupPresenter.CreateBuildingButtonClicked);
        }

        private void GoToMergePanel()
        {
            _createBuildingPopupPresenter.CLoseScroller();
            mergePanel.SetActive(true);
            closeButton.gameObject.SetActive(true);
            ClosePanelButtons();
        }

        public void OpenMergeButton()
        {
            goToMergePanelButton.gameObject.SetActive(true);
            createBuildingButton.gameObject.SetActive(false);
        }

        public void OpenCreateBuildingButton()
        {
            createBuildingButton.gameObject.SetActive(true);
            goToMergePanelButton.gameObject.SetActive(false);
        }

        public void ClosePanelButtons()
        {
            goToMergePanelButton.gameObject.SetActive(false);
            createBuildingButton.gameObject.SetActive(false);
        }
    }
}