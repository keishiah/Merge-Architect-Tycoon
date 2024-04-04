using System.Collections.Generic;
using Zenject;

public class QuestsPresenter
{
    private QuestPanel _questPanel;
    private QuestsProvider _questsProvider;
    public PlayerProgress _playerProgress;


    [Inject]
    void Construct(PlayerProgress playerProgress, QuestPanel questPanel,
        QuestsProvider questsProvider)
    {
        _playerProgress = playerProgress;
        _questPanel = questPanel;
        _questsProvider = questsProvider;
    }

    public void OpenQuestPopup()
    {
        _questPanel.CloseQuestElements();
        ShowQuests(_playerProgress.Quests.ActiveQuests);
    }
    private void ShowQuests(IEnumerable<QuestData> quests)
    {
        foreach (QuestData quest in quests)
        {
            _questPanel.SetQuestElement(quest);
        }
    }
}