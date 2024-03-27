using System.Collections.Generic;
using System.Linq;
using Zenject;

public class QuestGiver : IInitializableOnSceneLoaded
{
    private int _tutorialQuestsCount;

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
        ActivateQuestsOnStart();

        _tutorialQuestsCount =
            _staticDataService.Quests.Count(quest => quest.GiveQuestCondition == GiveQuestCondition.Tutorial);

        //_playerProgress.Quests.SubscribeToQuestCompleted(CheckBaseQuestsActivation);

        // _playerProgressService.Quests.SubscribeToQuestValueChanged(CheckBaseQuestsActivation);
        // CheckAllQuestsForActivation();
        // _questsProvider.GetQuestsWaitingForClaim.ObserveRemove().Subscribe(_ => { CheckBaseQuestsActivation(); });
    }

    public void CheckAllQuestsForActivation()
    {
        foreach (QuestData quest in _playerProgress.Quests.ActiveQuests)
        {
            _questsProvider.ActivateQuest(quest);
        }
    }

    private void CheckBaseQuestsActivation()
    {
        if (_tutorialQuestsCount > _playerProgress.Quests.CompletedQuests.Count)
            return;
        CheckAllQuestsForActivation();
    }

    private void ActivateQuestsOnStart()
    {
        List<BaseQuestInfo> questInfo = _staticDataService.Quests;

        foreach (QuestData quest in _playerProgress.Quests.ActiveQuests)
        {
            //_questsProvider.ActiveQuest(quest);
        }
    }
}