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
        CreateCurrentQuestsByConditionList();
        _questsProvider.OnQuestRemoved.Subscribe(QuestCompleted);
    }

    public void ActivateTutorialQuest(string questName)
    {
        if (!GetNextQuestByCondition(GiveQuestCondition.Tutorial, out Quest tutorialQuest)) 
            return;
        if (tutorialQuest.questName == questName)
        {
            ActivateNextQuest(tutorialQuest);
        }
    }

    private void QuestCompleted(GiveQuestCondition questsByCondition)
    {

        if (!_tutorialQuestsCompleted && !GetNextQuestByCondition(GiveQuestCondition.Tutorial, out Quest tutorialQuest))
        {
            _tutorialQuestsCompleted = true;
            ActivateBaseQuests();
        }
        else if (_tutorialQuestsCompleted)
        {
            if (GetNextQuestByCondition(questsByCondition, out Quest quest))
                ActivateNextQuest(quest);
        }
    }

    private void ActivateBaseQuests()
    {
        foreach (var questByCondition in _staticDataService.Quests)
        {
            if (questByCondition.Key != GiveQuestCondition.Tutorial)
            {
                GetNextQuestByCondition(questByCondition.Key, out Quest quest);
                ActivateNextQuest(quest);
            }
        }
    }

    private bool GetNextQuestByCondition(GiveQuestCondition questsByCondition, out Quest quest)
    {
        var questProgress = _playerProgressService.Progress.Quests;
        foreach (Quest nextQuest in _staticDataService.Quests[questsByCondition])
        {
            if (!questProgress.CompletedQuests.Contains(nextQuest.questId))
            {
                quest = nextQuest;
                return true;
            }
        }

        quest = default;
        return false;
    }

    private void ActivateNextQuest(Quest quest)
    {
        _questsProvider.ActivateQuest(quest);
    }

    private void CreateCurrentQuestsByConditionList()
    {
        var questProgress = _playerProgressService.Progress.Quests;

        //Создаем список первых невыполненных квестов каждого типа

        foreach (var questCondition in _staticDataService.Quests.Keys)
        {
            if (GetNextQuestByCondition(questCondition, out Quest currentQuest))
            {
                if (questProgress.ActiveQuests.Contains(currentQuest.questId))
                {
                    _questsProvider.AddActiveQuest(currentQuest);
                }
                else if (questProgress.QuestsWaitingForClaim.Contains(currentQuest.questId))
                {
                    _questsProvider.AddQuestWaitingForClaim(currentQuest);
                }
            }
        }
    }
}