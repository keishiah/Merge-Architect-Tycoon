using System.Collections.Generic;
using Zenject;


public class QuestsProvider : IInitializableOnSceneLoaded
{
    private readonly List<QuestBase> _activeQuests = new();
    public List<QuestBase> GetActiveQuestsList => _activeQuests;

    private readonly List<QuestBase> _questsWaitingForClaim = new();
    public List<QuestBase> GetQuestsWaitingForClaim => _questsWaitingForClaim;

    IPlayerProgressService _playerProgressService;

    [Inject]
    void Construct(IPlayerProgressService playerProgressService)
    {
        _playerProgressService = playerProgressService;
    }

    public void OnSceneLoaded()
    {
        _playerProgressService.Quests.SubscribeToQuestValueChanged(CheckAllQuestsCompleted);
    }

    public void ActivateQuest(QuestBase questBase)
    {
        // if (_activeQuests.Contains(questBase))
        //     return;
        _activeQuests.Add(questBase);
        _playerProgressService.Quests.AddActiveQuest(questBase.questId);
        questBase.InitializeRewardsAndItems();
    }

    public void AddQuestWaitingForClaim(QuestBase questBase)
    {
        questBase.InitializeRewardsAndItems();
        _questsWaitingForClaim.Add(questBase);
    }

    public void AddActiveQuest(QuestBase questBase)
    {
        _activeQuests.Add(questBase);
        questBase.InitializeRewardsAndItems();
    }

    public void CheckAllQuestsCompleted()
    {
        List<QuestBase> completedQuests = _activeQuests.FindAll(quest => quest.IsCompleted(_playerProgressService));
        foreach (QuestBase completedQuest in completedQuests)
        {
            _questsWaitingForClaim.Add(completedQuest);
            _activeQuests.Remove(completedQuest);
            _playerProgressService.Quests.AddQuestWaitingForClaim(completedQuest.questId);
        }
    }

    public void ClaimQuestReward(QuestBase questBase)
    {
        questBase.GiveReward(_playerProgressService);
        _playerProgressService.Quests.AddCompletedQuest(questBase.questId);

        _questsWaitingForClaim.Remove(questBase);
    }
}