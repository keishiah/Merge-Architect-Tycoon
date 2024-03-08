using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;


public class QuestsProvider : IInitializableOnSceneLoaded
{
    private Dictionary<GiveQuestCondition, Quest> _activeQuestsByCondition = new();

    private IPlayerProgressService _playerProgressService;

    public List<Quest> GetActiveQuestsList() => _activeQuestsByCondition.Values.ToList();

    private readonly Subject<GiveQuestCondition> _questRemovedSubject = new();
    public IObservable<GiveQuestCondition> OnQuestRemoved => _questRemovedSubject;
    private Dictionary<Quest, IDisposable> _questSubscriptions = new();


    [Inject]
    void Construct(IPlayerProgressService playerProgressService)
    {
        _playerProgressService = playerProgressService;
    }

    public void OnSceneLoaded()
    {
    }

    public void ActivateQuest(Quest questValue)
    {
        _activeQuestsByCondition.Add(questValue.giveQuestCondition, questValue);

        IDisposable subscription = SubscribeQuestProgress(questValue);
        _questSubscriptions.Add(questValue, subscription);
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

    private void CheckQuestCompleted(string buildingName)
    {
        foreach (Quest quest in _activeQuestsByCondition.Values.ToList())
        {
            if (quest.IsCompleted(buildingName))
            {
                quest.GiveReward();
                _activeQuestsByCondition.Remove(quest.giveQuestCondition);
                _playerProgressService.Progress.AddCompletedQuest(quest.questId);

                _questRemovedSubject.OnNext(quest.giveQuestCondition);
                if (_questSubscriptions.TryGetValue(quest, out var subscription))
                {
                    subscription.Dispose();
                    _questSubscriptions.Remove(quest);
                }
            }
        }
    }
}