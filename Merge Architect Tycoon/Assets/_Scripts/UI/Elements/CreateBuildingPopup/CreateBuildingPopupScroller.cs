using System.Collections.Generic;
using System.Linq;
using _Scripts.UI.Presenters;
using UnityEngine;
using Zenject;

namespace _Scripts.UI.Elements.CreateBuildingPopup
{
    public class CreateBuildingPopupScroller : UiViewBase
    {
        public List<CreateBuildingUiElement> createBuildingElements;
        public RectTransform scrollerRectTransform;
        private float _elementWidth;

        private CreateBuildingPopupPresenter _createBuildingPopupPresenter;
        private UiPresenter _uiPresenter;

        [Inject]
        void Construct(UiPresenter uiPresenter, CreateBuildingPopupPresenter createBuildingPopupPresenter)
        {
            _createBuildingPopupPresenter = createBuildingPopupPresenter;
            _uiPresenter = uiPresenter;
        }

        public override void InitUiElement(UiPresenter uiPresenter)
        {
            gameObject.SetActive(false);
            uiPresenter.AddUiElementToElementsList(this);
        }

        public void InitializeScroller()
        {
            InitUiElement(_uiPresenter);
            _createBuildingPopupPresenter.InitializeBuildingElements(createBuildingElements);
            _elementWidth = createBuildingElements[0].GetComponent<RectTransform>().rect.width;
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

        public void SetContentWidth()
        {
            scrollerRectTransform.sizeDelta = new Vector2(_elementWidth * createBuildingElements.Count,
                scrollerRectTransform.sizeDelta.y);
        }
    }
}