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
    private float _rectHeight;
    private float _startPosition;

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
        _rectHeight = _panelRectTransform.rect.height;
        _startPosition = _panelRectTransform.anchoredPosition.y;

    }

    public void OpenPanel()
    {
        if (resourcesPanel.gameObject.activeSelf)
            return;
        _panelRectTransform.gameObject.SetActive(true);
        _panelRectTransform.DOAnchorPosY(_startPosition + _rectHeight, 1)
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