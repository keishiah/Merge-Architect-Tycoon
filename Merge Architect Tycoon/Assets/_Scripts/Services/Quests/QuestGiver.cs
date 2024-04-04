using System.Collections.Generic;
using Zenject;

public class QuestGiver : IInitializableOnSceneLoaded
{
    private QuestsProvider _questsProvider;
    private StaticDataService _staticDataService;
    private PlayerProgress _playerProgress;

    [Inject]
    void Construct(QuestsProvider questsProvider, StaticDataService staticDataService,
        PlayerProgress playerProgress)
    {
        _questsProvider = questsProvider;
        _staticDataService = staticDataService;
        _playerProgress = playerProgress;
    }

    public void OnSceneLoaded()
    {
        CheckAllQuestsForActivation();
        ActivateQuestsOnStart();
    }

    private void CheckAllQuestsForActivation()
    {
        foreach (BaseQuestInfo quest in _staticDataService.Quests)
        {
            if (_playerProgress.Quests.CompletedQuests.Contains(quest.name))
                continue;

            QuestData questData = _playerProgress.Quests.ActiveQuests.Find(x => x.QuestInfo.name == quest.name);
            if (questData != null)
                continue;

            if (quest.IsReadyToStart(_playerProgress))
                _questsProvider.ActivateQuest(quest.GetNewQuestData());
        }
    }

    private void ActivateQuestsOnStart()
    {
        foreach (QuestData quest in _playerProgress.Quests.ActiveQuests)
        {
            _questsProvider.ActivateQuest(quest);
        }
    }
}