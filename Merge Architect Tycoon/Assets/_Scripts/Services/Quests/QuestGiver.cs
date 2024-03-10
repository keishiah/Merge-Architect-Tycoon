using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

public class QuestGiver : IInitializableOnSceneLoaded
{
    private Dictionary<GiveQuestCondition, List<Quest>> _questsByCondition = new();

    private QuestsProvider _questsProvider;
    private IStaticDataService _staticDataService;
    private IPlayerProgressService _playerProgressService;

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
        var questProgress = _playerProgressService.Progress.Quests;

        foreach (var quest in _staticDataService.Quests)
        {
            GiveQuestCondition questCondition = quest.giveQuestCondition;
            if (!questProgress.CompletedQuests.Contains(quest.questId))
            {
                if (!_questsByCondition.ContainsKey(questCondition))
                {
                    _questsByCondition.Add(questCondition, new List<Quest> { });
                }

                _questsByCondition[questCondition].Add(quest);
            }

            if (questProgress.QuestsWaitingForClaim.Contains(quest.questId))
                _questsProvider.AddQuestWaitingForClaim(quest);
        }

        _questsProvider.OnQuestRemoved.Subscribe(QuestCompleted);
        ActivateNextQuest();
    }

    public void QuestCompleted(GiveQuestCondition questsByCondition)
    {
        _questsByCondition[questsByCondition].RemoveAt(0);
        if (_questsByCondition[GiveQuestCondition.Tutorial].Count > 0)
            ActivateQuest(_questsByCondition[GiveQuestCondition.Tutorial].First());
    }

    public bool GetCurrentQuest(GiveQuestCondition questCondition, out Quest quest)
    {
        if (_questsByCondition.ContainsKey(questCondition))
        {
            quest = _questsByCondition[questCondition].First();
            return true;
        }

        quest = null;
        return false;
    }


    private void ActivateNextQuest()
    {
        // ActivateTutorialQuests();
    }

    public void ActivateTutorialQuests(string questName)
    {
        if (_questsByCondition.TryGetValue(GiveQuestCondition.Tutorial, out var tutorialQuests))
            if (tutorialQuests[0].questName == questName)
            {
                ActivateQuest(tutorialQuests[0]);
            }
    }


    private void ActivateQuest(Quest quest)
    {
        _questsProvider.ActivateQuest(quest);
    }
}