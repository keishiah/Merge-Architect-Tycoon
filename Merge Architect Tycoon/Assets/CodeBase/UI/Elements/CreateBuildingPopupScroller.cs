using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class CreateBuildingPopupScroller : UiViewBase
    {
        public List<CreateBuildingUiElement> createBuildingElements;

        private UiPresenter _uiPresenter;
        private CreateBuildingPopupPresenter _createBuildingPopupPresenter;
        private bool _popupInitialized = false;

        public CreateBuildingPopupScroller()
        {
            _popupInitialized = false;
        }

        [Inject]
        void Construct(UiPresenter uiPresenter, CreateBuildingPopupPresenter createBuildingPopupPresenter)
        {
            _uiPresenter = uiPresenter;
            _createBuildingPopupPresenter = createBuildingPopupPresenter;
            InitUiElement(uiPresenter);
            gameObject.SetActive(false);
        }

        public override void InitUiElement(UiPresenter uiPresenter)
        {
            uiPresenter.AddUiElementToElementsList(this);

            _uiPresenter = uiPresenter;
            _createBuildingPopupPresenter.InitializeScroller(this);
        }

        public void SortBuildingElements()
        {
            if (!_popupInitialized)
            {
                _createBuildingPopupPresenter.InitializeBuildingElements(createBuildingElements);
                _createBuildingPopupPresenter.SetBuildingElements();
                _createBuildingPopupPresenter.SortBuildingElements();
                _createBuildingPopupPresenter.RenderSortedList();
            }
            else
            {
                // _createBuildingPopupPresenter.SortBuildingElements();
                _createBuildingPopupPresenter.RenderSortedList();
            }
        }
    }
}