using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;


public class QuestsProvider : IInitializableOnSceneLoaded
{
    private readonly List<Quest> _activeQuests = new();
    public List<Quest> GetActiveQuestsList => _activeQuests;

    private readonly List<Quest> _questsWaitingForClaim = new();
    public List<Quest> GetQuestsWaitingForClaim => _questsWaitingForClaim;

    private readonly Subject<GiveQuestCondition> _questRemovedSubject = new();
    public IObservable<GiveQuestCondition> OnQuestRemoved => _questRemovedSubject;

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
        _activeQuests.Add(quest);
        _playerProgressService.Progress.AddActiveQuest(quest.questId);
        quest.InitializeRewardsAndItems();
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

    public void CheckCompletionTutorialQuest(string questName)
    {
        if (_activeQuests.Count(quest => quest.questName == questName) > 0)
        {
            var quest = _activeQuests.Find(quest => quest.questName == questName);
            _questsWaitingForClaim.Add(quest);
            _activeQuests.Remove(quest);
            _playerProgressService.Progress.AddQuestWaitingForClaim(quest.questId);
        }
    }

    public void ClaimQuestReward(Quest quest)
    {
        quest.GiveReward(_playerProgressService.Progress);
        _playerProgressService.Progress.AddCompletedQuest(quest.questId);

        _questsWaitingForClaim.Remove(quest);
        _questRemovedSubject.OnNext(quest.giveQuestCondition);
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
                _playerProgressService.Progress.AddQuestWaitingForClaim(quest.questId);
                _activeQuests.Remove(quest);
                _playerProgressService.Progress.ClearMergeCount();
            }
        }
    }
}