using _Scripts.UI.Presenters;
using CodeBase.UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI.Elements.CreateBuildingPopup
{
    public class CreateBuildingPopup : MonoBehaviour
    {
        public Button goToMergePanelButton;
        public Button createBuildingButton;

        private SceneButtons _sceneButtons;
        private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

        [Inject]
        void Construct(CreateBuildingPopupPresenter createBuildingPopupPresenter,
            SceneButtons _sceneButtons)
        {
            this._sceneButtons = _sceneButtons;
            _createBuildingPopupPresenter = createBuildingPopupPresenter;
            _createBuildingPopupPresenter.InitializePopup(this);

            goToMergePanelButton.onClick.AddListener(GoToMergePanel);
            createBuildingButton.onClick.AddListener(_createBuildingPopupPresenter.CreateBuildingButtonClicked);
        }

        private void GoToMergePanel()
        {
            _sceneButtons.OnMenuButtonClick(SceneButtonsEnum.Merge);
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
    }
}