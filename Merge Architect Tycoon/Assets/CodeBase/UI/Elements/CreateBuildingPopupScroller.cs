using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class CreateBuildingPopupScroller : UiViewBase
    {
        public List<CreateBuildingUiElement> createBuildingElements;

        private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

        [Inject]
        void Construct(UiPresenter uiPresenter, CreateBuildingPopupPresenter createBuildingPopupPresenter)
        {
            _createBuildingPopupPresenter = createBuildingPopupPresenter;
            InitUiElement(uiPresenter);
            gameObject.SetActive(false);
        }

        public override void InitUiElement(UiPresenter uiPresenter)
        {
            uiPresenter.AddUiElementToElementsList(this);

            _createBuildingPopupPresenter.InitializeScroller(this);
            _createBuildingPopupPresenter.InitializeBuildingElements(createBuildingElements);
        }

        public void SortBuildingElements()
        {
            _createBuildingPopupPresenter.RenderSortedList();
        }

        public void RemoveBuildingElementFromPopup(string buildingName)
        {
            var removingElement =
                createBuildingElements.FirstOrDefault(element => element.buildingName == buildingName);
            if (removingElement)
                removingElement.gameObject.SetActive(false);
        }
    }
}