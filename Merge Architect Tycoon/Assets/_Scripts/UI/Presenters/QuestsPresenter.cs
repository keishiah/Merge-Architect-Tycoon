using System.Collections.Generic;
using Zenject;

public class QuestsPresenter
{
    private QuestPanel _questPanel;
    public PlayerProgress _playerProgress;


    [Inject]
    void Construct(PlayerProgress playerProgress, QuestPanel questPanel)
    {
        _playerProgress = playerProgress;
        _questPanel = questPanel;
    }

    public void OpenQuestPopup()
    {
        _questPanel.CloseQuestElements();
        _questPanel.ShowQuests(_playerProgress.Quests.ActiveQuests);
    }
}