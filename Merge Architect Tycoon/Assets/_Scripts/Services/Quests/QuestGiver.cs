using UniRx;
using Zenject;

public class QuestGiver : IInitializableOnSceneLoaded
{
    private QuestsProvider _questsProvider;

    private IStaticDataService _staticDataService;

    private IPlayerProgressService _playerProgressService;

    private bool _tutorialQuestsCompleted;

    [Inject]
    void Construct(QuestsProvider questsProvider, IStaticDataService staticDataService,
        IPlayerProgressService playerProgressService)
    {
        _questsProvider = questsProvider;
        _staticDataService = staticDataService;
        _playerProgressService = playerProgressService;
    }

    public void OnSceneLoaded()
    {
        // _questsProvider.GetQuestsWaitingForClaim.ObserveRemove().Subscribe(_ => { CheckAllQuestsForActivation(); });
        // CheckAllQuestsForActivation();
        ActivateQuestsOnStart();
    }

    public void CheckAllQuestsForActivation()
    {
        foreach (Quest quest in _staticDataService.Quests)
        {
            if (!_questsProvider.GetActiveQuestsList.Contains(quest) &&
                !_questsProvider.GetQuestsWaitingForClaim.Contains(quest) &&
                !_playerProgressService.Quests.CompletedQuests.Contains(quest.questId))
            {
                if (quest.IsReadyToStart(_playerProgressService))
                {
                    _questsProvider.ActivateQuest(quest);
                }
            }
        }
    }


    private void ActivateQuestsOnStart()
    {
        var questProgress = _playerProgressService.Quests;

        foreach (var quest in _staticDataService.Quests)
        {
            if (questProgress.ActiveQuests.Contains(quest.questId))
            {
                _questsProvider.AddActiveQuest(quest);
            }
            else if (questProgress.QuestsWaitingForClaim.Contains(quest.questId))
            {
                _questsProvider.AddQuestWaitingForClaim(quest);
            }
        }
    }
}