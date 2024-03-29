using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CreateBuildingPopup : MonoBehaviour
{
    public ResourcesPanel resourcesPanel;

    private RectTransform _panelRectTransform;
    private Vector2 _panelStartPosition;
    private MenuButtonsWidgetController _sceneButtons;
    private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

    [Inject]
    void Construct(CreateBuildingPopupPresenter createBuildingPopupPresenter,
        MenuButtonsWidgetController _sceneButtons)
    {
        this._sceneButtons = _sceneButtons;
        _createBuildingPopupPresenter = createBuildingPopupPresenter;
    }

    public void InitializePopup()
    {
        _panelRectTransform = resourcesPanel.GetComponent<RectTransform>();
        _panelStartPosition = _panelRectTransform.anchoredPosition;
        resourcesPanel.actionButton.onClick.AddListener(_createBuildingPopupPresenter.CreateBuildingButtonClicked);
    }

    public void OpenPanel()
    {
        if (resourcesPanel.isActiveAndEnabled)
            return;
        _panelRectTransform.gameObject.SetActive(true);
        _panelRectTransform.DOAnchorPosY(_panelRectTransform.anchoredPosition.y + _panelRectTransform.rect.height, 1)
            .SetEase(Ease
                .OutBounce);
    }

    public void HideButtons()
    {
        _panelRectTransform.gameObject.SetActive(false);
        _panelRectTransform.anchoredPosition = _panelStartPosition;
    }

    private void GoToMergePanel()
    {
        _sceneButtons.OnMenuButtonClick(MenuButtonsEnum.Merge);
    }
}