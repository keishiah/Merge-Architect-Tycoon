using Zenject;

public class QuestsProvider
{
    private PlayerProgress _playerProgress;
    private PlayerProgressService _playerProgressService;

    [Inject]
    void Construct(PlayerProgress playerProgress, PlayerProgressService playerProgressService)
    {
        _playerProgress = playerProgress;
        _playerProgressService = playerProgressService;
    }

    public void ActivateQuest(QuestData quest)
    {
        quest.Subscribe(_playerProgress);
        _playerProgressService.AddQuest(quest);
        //questBase.OnComplete += CheckQuestsCompleted;
    }

    public void ClaimQuestReward(QuestData quest)
    {
        quest.QuestInfo.GiveReward(_playerProgressService);
        _playerProgressService.QuestComplete(quest);
    }
}