using System.Collections.Generic;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class CreateBuildingPopup : UiViewBase
    {
        public List<CreateBuildingUiElement> createBuildingElements;

        private UiPresenter _uiPresenter;
        private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

        [Inject]
        void Construct(UiPresenter uiPresenter, CreateBuildingPopupPresenter createBuildingPopupPresenter)
        {
            _uiPresenter = uiPresenter;
            _createBuildingPopupPresenter = createBuildingPopupPresenter;
            InitUiElement(_uiPresenter);
        }

        public override void InitUiElement(UiPresenter uiPresenter)
        {
            uiPresenter.AddUiElementToElementsList(this);

            _uiPresenter = uiPresenter;
            _createBuildingPopupPresenter.SetupPopup(this);
            gameObject.SetActive(false);
        }

        public void InitPopupElements()
        {
            _createBuildingPopupPresenter.SetUpBuildingButtons(createBuildingElements);
        }
    }
}