using Zenject;

public class QuestWidget : WidgetView
{
    [Inject] private QuestsPresenter _questsPresenter;

    public override void OnOpen()
    {
        base.OnOpen();
        _questsPresenter.OpenQuestPopup();
    }
}