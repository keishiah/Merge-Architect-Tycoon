using UnityEngine;
using Zenject;

public class QuestWidget : Widget
{
    [Inject] private QuestsPresenter _questsPresenter;

    public override void OnOpen()
    {
        base.OnOpen();
        _questsPresenter.OpenQuestPopup();
    }
}