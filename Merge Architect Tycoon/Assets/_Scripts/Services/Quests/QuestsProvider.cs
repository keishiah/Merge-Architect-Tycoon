using System;
using UnityEngine;
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
        InitActiveQuests();
        CheckAllQuestsForActivation();
    }

    private void InitActiveQuests()
    {
        foreach(QuestData quest in _playerProgress.Quests.ActiveQuests)
        {
            QuestInfo questInfo = _staticDataService.Quests.Find(q => q.name == quest.QuestID);
            questInfo.SetParams(quest);
            ActivateQuest(quest);
        }
    }

    public void CheckAllQuestsForActivation()
    {
        foreach (QuestInfo quest in _staticDataService.Quests)
        {
            if (_playerProgress.Quests.CompletedQuests.Contains(quest.name))
                continue;

            QuestData questData = _playerProgress.Quests.ActiveQuests.Find(x => x.QuestInfo.name == quest.name);
            if (questData != null)
                continue;

            if (quest.IsReadyToStart(_playerProgress))
                ActivateQuest(quest);
        }
    }
    public void ActivateQuest(QuestInfo quest)
    {
        ActivateQuest(quest.GetNewQuestData());
    }
    public void ActivateQuest(QuestData quest)
    {
        _playerProgressService.ActivateQuest(quest);
    }
}