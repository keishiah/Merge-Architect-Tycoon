using System.Collections.Generic;

namespace CodeBase.UI.Elements
{
    public class CreateBuildingPopup : UiViewBase
    {
        public List<CreateBuildingButton> createBuildingButtons;

        private UiPresenter _uiPresenter;
        private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

        public override void InitUiElement(UiPresenter uiPresenter)
        {
            uiPresenter.AddUiElementToElementsList(this);

            _uiPresenter = uiPresenter;
            _createBuildingPopupPresenter = uiPresenter._createBuildingPopupPresenter;
            gameObject.SetActive(false);
        }

        public void InitPopupButtons()
        {
            _createBuildingPopupPresenter.SetUpBuildingButtons(createBuildingButtons);
        }
    }
}