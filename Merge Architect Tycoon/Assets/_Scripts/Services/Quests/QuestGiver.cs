using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

public class QuestGiver : IInitializableOnSceneLoaded
{
    private Dictionary<GiveQuestCondition, Quest> _currentQuestByCondition = new();

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
        ActivateNextQuest();
    }

    public void ActivateTutorialQuests(string questName)
    {
        if (GetNextQuestByCondition(GiveQuestCondition.Tutorial, out Quest tutorialQuest))
            if (tutorialQuest.questName == questName)
            {
                ActivateQuest(tutorialQuest);
            }
    }

    private void QuestCompleted(GiveQuestCondition questsByCondition)
    {
        _currentQuestByCondition.Remove(questsByCondition);

        if (questsByCondition != GiveQuestCondition.Tutorial)
        {
            if (GetNextQuestByCondition(questsByCondition, out Quest quest))
                ActivateQuest(quest);
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


    private void ActivateNextQuest()
    {
        if (_tutorialQuestsCompleted)
        {
            //Активировать необходимые квесты если туториал пройден
        }
    }


    private void ActivateQuest(Quest quest)
    {
        _questsProvider.ActivateQuest(quest);
    }

    private void CreateCurrentQuestsByConditionList()
    {
        var questProgress = _playerProgressService.Progress.Quests;

        //Создаем список первых невыполненных квестов каждого типа

        if (!GetNextQuestByCondition(GiveQuestCondition.Tutorial, out Quest tutorialQuest))
            _tutorialQuestsCompleted = true;

        foreach (var questCondition in _staticDataService.Quests.Keys)
        {
            if (GetNextQuestByCondition(questCondition, out Quest currentQuest))
            {
                _currentQuestByCondition[questCondition] = currentQuest;
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