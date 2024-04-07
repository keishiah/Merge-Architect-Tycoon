using Zenject;

public class QuestsProvider : IInitializableOnSceneLoaded
{
    private PlayerProgress _playerProgress;
    private PlayerProgressService _playerProgressService;
    private StaticDataService _staticDataService;

    [Inject]
    void Construct(PlayerProgress playerProgress, PlayerProgressService playerProgressService,
        StaticDataService staticDataService)
    {
        _playerProgress = playerProgress;
        _playerProgressService = playerProgressService;
        _staticDataService = staticDataService;
    }

    public void OnSceneLoaded()
    {
        CheckAllQuestsForActivation();
    }

    private void CheckAllQuestsForActivation()
    {
        foreach (BaseQuestInfo quest in _staticDataService.Quests)
        {
            if (_playerProgress.Quests.CompletedQuests.Contains(quest.name))
                continue;

            QuestData questData = _playerProgress.Quests.ActiveQuests.Find(x => x.QuestInfo.name == quest.name);
            if (questData != null)
            {
                ActivateQuest(questData);
                continue;
            }

            if (quest.IsReadyToStart(_playerProgress))
                ActivateQuest(quest.GetNewQuestData());
        }
    }

    public void ActivateQuest(QuestData quest)
    {
        quest.Subscribe(_playerProgress);
        _playerProgressService.AddQuest(quest);
    }

    public void ClaimQuestReward(QuestData quest)
    {
        quest.GiveReward(_playerProgressService);
        _playerProgressService.QuestComplete(quest);
    }
}