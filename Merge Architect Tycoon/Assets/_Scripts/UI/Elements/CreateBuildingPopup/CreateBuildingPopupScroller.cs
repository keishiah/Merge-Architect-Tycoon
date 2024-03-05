using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CreateBuildingPopupScroller : UiViewBase
{
    public List<CreateBuildingUiElement> createBuildingElements;
    public RectTransform scrollerRectTransform;
    private float _elementWidth;

    private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

    [Inject]
    void Construct(CreateBuildingPopupPresenter createBuildingPopupPresenter)
    {
        _createBuildingPopupPresenter = createBuildingPopupPresenter;
    }

    public override void InitUiElement(UiPresenter uiPresenter)
    {
        gameObject.SetActive(false);
    }

    public void InitializeScroller()
    {
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