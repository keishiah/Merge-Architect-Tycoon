using Zenject;

public class CreateBuildingWidget : Widget
{
    [Inject]
    private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

    public override void OnEnable()
    {
        base.OnEnable();
        _createBuildingPopupPresenter.OpenScroller();
    }
}