using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;


public class QuestsProvider
{
    private Dictionary<GiveQuestCondition, Quest> _activeQuestsByCondition = new();

    private IPlayerProgressService _playerProgressService;
    public List<Quest> GetActiveQuestsList() => _activeQuestsByCondition.Values.ToList();

    private List<Quest> _questsWaitingForClaim = new();
    public List<Quest> GetQuestsWaitingForClaim() => _questsWaitingForClaim;

    private readonly Subject<GiveQuestCondition> _questRemovedSubject = new();
    public IObservable<GiveQuestCondition> OnQuestRemoved => _questRemovedSubject;
    private Dictionary<Quest, IDisposable> _questSubscriptions = new();


    [Inject]
    void Construct(IPlayerProgressService playerProgressService)
    {
        _playerProgressService = playerProgressService;
    }

    public void ActivateQuest(Quest quest)
    {
        _activeQuestsByCondition.Add(quest.giveQuestCondition, quest);
        quest.InitializeRewardsAndItems();

        IDisposable subscription = SubscribeQuestProgress(quest);
        _questSubscriptions.Add(quest, subscription);
    }

    public void ClaimQuestReward(Quest quest)
    {
        quest.GiveReward(_playerProgressService.Progress);
        _activeQuestsByCondition.Remove(quest.giveQuestCondition);
        _playerProgressService.Progress.AddCompletedQuest(quest.questId);

        _questRemovedSubject.OnNext(quest.giveQuestCondition);
        if (_questSubscriptions.TryGetValue(quest, out var subscription))
        {
            subscription.Dispose();
            _questSubscriptions.Remove(quest);
        }
    }

    private IDisposable SubscribeQuestProgress(Quest quest)
    {
        switch (quest.questType)
        {
            case QuestType.Building:
                return _playerProgressService.Progress.Buldings.SubscribeToBuildingsChanges(CheckQuestCompleted);
                break;
            case QuestType.Merge:
                break;
            default:
                return null;
        }

        return null;
    }

    public void AddQuestWaitingForClaim(Quest quest)
    {
        _questsWaitingForClaim.Add(quest);
    }

    private void CheckQuestCompleted(string buildingName)
    {
        foreach (Quest quest in _activeQuestsByCondition.Values.ToList())
        {
            if (quest.IsCompleted(buildingName))
            {
                _questsWaitingForClaim.Add(quest);
                _playerProgressService.Progress.AddQuestWaitingForClaim(quest.questId);
            }
        }
    }
}