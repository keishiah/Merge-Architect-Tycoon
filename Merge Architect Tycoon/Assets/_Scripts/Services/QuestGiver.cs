using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestGiver : IInitializableOnSceneLoaded
{
    private Dictionary<string, Quest> _currentQuestsDictionary = new();

    private QuestsPresenter _questsPresenter;
    private QuestsProvider _questsProvider;
    private IPlayerProgressService _playerProgressService;

    [Inject]
    void Construct(QuestsPresenter questsPresenter, QuestsProvider questsProvider,
        IPlayerProgressService playerProgressService)
    {
        _questsPresenter = questsPresenter;
        _questsProvider = questsProvider;
        _playerProgressService = playerProgressService;
    }

    public void OnSceneLoaded()
    {
        ActivateQuests();
    }

    private void ActivateQuests()
    {
        ActivateFirstQuest();

        var secondQuest = _questsProvider.GetSecondQuest();
        _questsPresenter.ActivateBuildingQuestQuest(secondQuest);
    }

    private void ActivateFirstQuest()
    {
        var firstQuest = _questsProvider.GetFirstQuest();
        _currentQuestsDictionary.Add(firstQuest.questId, firstQuest);

        ActivateQuest(firstQuest);
    }


    private void ActivateQuest(Quest quest)
    {
        switch (quest.questType)
        {
            case QuestType.BuildingQuest:
                _questsPresenter.ActivateBuildingQuestQuest(quest);
                var buildingProgress = _playerProgressService.Progress.Buldings;
                buildingProgress.SubscribeToBuildingsChanges(OnBuildingCreated);
                break;
        }
    }

    private void OnBuildingCreated(string buildingName)
    {
        foreach (var quest in _currentQuestsDictionary.Values)
        {
            if (quest.questType == QuestType.BuildingQuest && quest.buildingName == buildingName)
            {
                // _questsPresenter.CompleteBuildingQuest(quest);
            }
        }
    }
}