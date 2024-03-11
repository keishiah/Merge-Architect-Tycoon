using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;


public class QuestsProvider : IInitializableOnSceneLoaded
{
    private readonly List<Quest> _activeQuests = new();
    public List<Quest> GetActiveQuestsList => _activeQuests;
    private readonly ReactiveCollection<Quest> _questsWaitingForClaim = new();
    public ReactiveCollection<Quest> GetQuestsWaitingForClaim => _questsWaitingForClaim;
    private IPlayerProgressService _playerProgressService;

    [Inject]
    void Construct(IPlayerProgressService playerProgressService)
    {
        _playerProgressService = playerProgressService;
    }

    public void OnSceneLoaded()
    {
        _playerProgressService.Progress.Quests.SubscribeToMerge(CheckMergeQuestCompleted);
    }

    public void ActivateQuest(Quest quest)
    {
        if (!_activeQuests.Contains(quest))
        {
            _activeQuests.Add(quest);
            _playerProgressService.Progress.Quests.AddActiveQuest(quest.questId);
            quest.InitializeRewardsAndItems();
        }
    }

    public void AddQuestWaitingForClaim(Quest quest)
    {
        quest.InitializeRewardsAndItems();
        _questsWaitingForClaim.Add(quest);
    }

    public void AddActiveQuest(Quest quest)
    {
        _activeQuests.Add(quest);
        quest.InitializeRewardsAndItems();
    }

    public void CheckCompletionOfTutorialQuest(string questName)
    {
        if (_activeQuests.Count(quest => quest.questName == questName) <= 0)
            return;
        {
            var quest = _activeQuests.Find(quest => quest.questName == questName);
            _questsWaitingForClaim.Add(quest);
            _activeQuests.Remove(quest);
            _playerProgressService.Progress.Quests.AddQuestWaitingForClaim(quest.questId);
        }
    }

    public void ClaimQuestReward(Quest quest)
    {
        quest.GiveReward(_playerProgressService.Progress);
        _playerProgressService.Progress.Quests.AddCompletedQuest(quest.questId);

        _questsWaitingForClaim.Remove(quest);
    }


    private void CheckMergeQuestCompleted(int mergeCount)
    {
        var progressQuests = _playerProgressService.Progress;
        if (_activeQuests.Count(quest => quest.giveQuestCondition == GiveQuestCondition.Merge) > 0)
        {
            var quest = _activeQuests.Find(quest => quest.giveQuestCondition == GiveQuestCondition.Merge);
            if (quest.IsCompleted(progressQuests.Quests.CurrentMergeCount))
            {
                _questsWaitingForClaim.Add(quest);
                progressQuests.Quests.AddQuestWaitingForClaim(quest.questId);
                _activeQuests.Remove(quest);
                _playerProgressService.Progress.ClearMergeCount();
            }
        }
    }
}