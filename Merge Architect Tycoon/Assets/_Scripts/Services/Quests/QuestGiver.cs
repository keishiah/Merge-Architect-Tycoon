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
        _questsProvider.GetQuestsWaitingForClaim.ObserveRemove().Subscribe(_ => { CheckAllQuestsForActivation(); });
        ActivateQuestsOnStart();
        CheckAllQuestsForActivation();
    }

    public void CheckAllQuestsForActivation()
    {
        foreach (Quest quest in _staticDataService.Quests)
        {
            Progress progress = _playerProgressService.Progress;

            if (!_questsProvider.GetActiveQuestsList.Contains(quest) &&
                !_questsProvider.GetQuestsWaitingForClaim.Contains(quest) &&
                !progress.Quests.CompletedQuests.Contains(quest.questId))
            {
                if (quest.IsReadyToStart(progress))
                {
                    _questsProvider.ActivateQuest(quest);
                }
            }
        }
    }


    private void ActivateQuestsOnStart()
    {
        var questProgress = _playerProgressService.Progress.Quests;


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