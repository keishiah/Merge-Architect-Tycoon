using UnityEngine;
using Zenject;

public class CreateBuildingWidget : Widget
{
    [Inject] private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

    public override void OnOpen()
    {
        base.OnOpen();
        _createBuildingPopupPresenter.OpenScroller();
    }
}