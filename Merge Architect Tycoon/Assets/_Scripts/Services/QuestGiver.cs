using System.Collections.Generic;
using Zenject;

public class QuestGiver : IInitializableOnSceneLoaded
{
    private List<Quest> _currentQuests = new();

    private QuestsPresenter _questsPresenter;
    private QuestsProvider _questsProvider;

    [Inject]
    void Construct(QuestsPresenter questsPresenter, QuestsProvider questsProvider)
    {
        _questsPresenter = questsPresenter;
        _questsProvider = questsProvider;
    }

    public void OnSceneLoaded()
    {
        ActivateQuests();
    }

    private void ActivateQuests()
    {
        var firstQuest = _questsProvider.GetFirstQuest();
        _currentQuests.Add(firstQuest);
        _questsPresenter.ActivateQuest(firstQuest);

        var secondQuest = _questsProvider.GetSecondQuest();
        _currentQuests.Add(secondQuest);
        _questsPresenter.ActivateQuest(secondQuest);
    }
}