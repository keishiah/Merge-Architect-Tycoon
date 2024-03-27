using Zenject;

public class CreateBuildingWidget : WidgetView
{
    [Inject] private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

    public override void OnOpen()
    {
        base.OnOpen();
        _createBuildingPopupPresenter.OpenScroller();
    }
}