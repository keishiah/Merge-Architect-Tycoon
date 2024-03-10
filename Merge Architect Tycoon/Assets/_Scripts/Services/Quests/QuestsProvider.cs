using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;


public class QuestsProvider
{
    private Dictionary<GiveQuestCondition, Quest> _activeQuestsByCondition = new();
    public List<Quest> GetActiveQuestsList() => _activeQuestsByCondition.Values.ToList();

    private List<Quest> _questsWaitingForClaim = new();
    public List<Quest> GetQuestsWaitingForClaim() => _questsWaitingForClaim;

    private readonly Subject<GiveQuestCondition> _questRemovedSubject = new();
    public IObservable<GiveQuestCondition> OnQuestRemoved => _questRemovedSubject;

    private IPlayerProgressService _playerProgressService;

    [Inject]
    void Construct(IPlayerProgressService playerProgressService)
    {
        _playerProgressService = playerProgressService;
    }

    public void SubscribeProvider()
    {
        _playerProgressService.Progress.Buldings.SubscribeToBuildingsChanges(CheckQuestCompleted);
    }

    public void ActivateQuest(Quest quest)
    {
        _activeQuestsByCondition.Add(quest.giveQuestCondition, quest);
        quest.InitializeRewardsAndItems();
    }

    public void ClaimQuestReward(Quest quest)
    {
        quest.GiveReward(_playerProgressService.Progress);
        _activeQuestsByCondition.Remove(quest.giveQuestCondition);
        _playerProgressService.Progress.AddCompletedQuest(quest.questId);

        _questRemovedSubject.OnNext(quest.giveQuestCondition);
    }


    public void AddQuestWaitingForClaim(Quest quest)
    {
        _questsWaitingForClaim.Add(quest);
    }

    public void CheckCompletionTutorialQuest(string questName)
    {
        foreach (Quest quest in _activeQuestsByCondition.Values.ToList())
        {
            if (quest.questName == questName)
            {
                _questsWaitingForClaim.Add(quest);
                _playerProgressService.Progress.AddQuestWaitingForClaim(quest.questId);
            }
        }
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