using Zenject;

public class QuestsPresenter
{
    private IPlayerProgressService _playerProgressService;

    private QuestPopup _questPopup;

    private QuestsProvider _questsProvider;

    [Inject]
    void Construct(IPlayerProgressService playerProgressService, QuestPopup questPopup,
        QuestsProvider questsProvider)
    {
        _playerProgressService = playerProgressService;
        _questPopup = questPopup;
        _questsProvider = questsProvider;
    }

    public void InitializePresenter()
    {
    }

    public void OpenQuestPopup()
    {
        _questPopup.gameObject.SetActive(true);
        CloseQuestElements();
        foreach (Quest quest in _questsProvider.GetActiveQuestsList())
        {
            ShowQuestInPopup(quest);
        }
    }

    private void ShowQuestInPopup(Quest quest)
    {
        if (_questPopup.GetInactiveQuestElement(out var questElement))
        {
            questElement.gameObject.SetActive(true);
            questElement.SetQuestText(quest.questName);
            questElement.RenderQuest(quest.GetRewardList(), quest);
        }
    }

    private void CloseQuestElements()
    {
        foreach (QuestElement questElement in _questPopup.questElements)
        {
            questElement.gameObject.SetActive(false);
        }
    }

    public void CompleteBuildingQuest(Quest quest)
    {
    }
}