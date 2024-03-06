using System.Collections.Generic;
using Zenject;

public class QuestGiver : IInitializableOnSceneLoaded
{
    private List<Quest> _currentQuests = new();

    private QuestsPresenter _questsPresenter;
    private QuestsProvider _questsProvider;
    private IPlayerProgressService _playerProgressService;

    [Inject]
    void Construct(QuestsPresenter questsPresenter, QuestsProvider questsProvider,
        IPlayerProgressService playerProgressService)
    {
        _questsPresenter = questsPresenter;
        _questsProvider = questsProvider;
        _playerProgressService = playerProgressService;
    }

    public void OnSceneLoaded()
    {
        ActivateQuests();
    }

    private void ActivateQuests()
    {
        ActivateFirstQuest();

        var secondQuest = _questsProvider.GetSecondQuest();
        _currentQuests.Add(secondQuest);
        _questsPresenter.ActivateQuest(secondQuest);
    }

    private void ActivateFirstQuest()
    {
        var firstQuest = _questsProvider.GetFirstQuest();
        _currentQuests.Add(firstQuest);
        _questsPresenter.ActivateQuest(firstQuest);
        var buildingProgress = _playerProgressService.Progress.Buldings;
        buildingProgress.SubscribeToBuildingsChanges(OnBuildingCreated);
    }
//todo create 2 dictionaries for 2 quest types, subscribe each changing.
// create method for each quest type to track quest completion and correspond them ro questpresenter
    private void OnBuildingCreated(string buildingName)
    {
        throw new System.NotImplementedException();
    }
}