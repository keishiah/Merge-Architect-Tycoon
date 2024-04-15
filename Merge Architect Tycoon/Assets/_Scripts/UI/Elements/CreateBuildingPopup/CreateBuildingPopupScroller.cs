using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CreateBuildingPopupScroller : MonoBehaviour
{
    public List<CreateBuildingUiElement> createBuildingElements;
    public ScrollRect scrollRect;
    private float _elementWidth;

    private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

    [Inject]
    void Construct(CreateBuildingPopupPresenter createBuildingPopupPresenter)
    {
        _createBuildingPopupPresenter = createBuildingPopupPresenter;
    }

    public void InitializeScroller()
    {
        _createBuildingPopupPresenter.InitializeBuildingElements(createBuildingElements);
    }

    public void OpenScroller()
    {
        SortBuildingElements();
        SetContentWidth();
        ScrollToTheLeftBorder();
    }

    public void RemoveBuildingElementFromPopup(string buildingName)
    {
        var removingElement =
            createBuildingElements.FirstOrDefault(element => element.buildingName == buildingName);
        if (removingElement)
            removingElement.gameObject.SetActive(false);
    }

    private void SortBuildingElements()
    {
        _createBuildingPopupPresenter.RenderSortedList();
    }

    private void ScrollToTheLeftBorder()
    {
        scrollRect.horizontalNormalizedPosition = 0f;
    }

    private void SetContentWidth()
    {
        _elementWidth = createBuildingElements[0].GetComponent<RectTransform>().rect.width;

        var activeElementsCount = createBuildingElements.Count(element => element.gameObject.activeSelf);

        scrollRect.content.sizeDelta =
            new Vector2(_elementWidth * activeElementsCount * .5f, scrollRect.content.sizeDelta.y);
    }
}