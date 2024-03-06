using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestsPresenter
{
    private IPlayerProgressService _playerProgressService;

    private QuestPopup _questPopup;
    private Dictionary<Quest, QuestElement> _questElements = new();

    private LoadLevelState _loadLevelState;
    private QuestsProvider _questsProvider;

    [Inject]
    void Construct(IPlayerProgressService playerProgressService, LoadLevelState loadLevelState, QuestPopup questPopup,
        QuestsProvider questsProvider)
    {
        _playerProgressService = playerProgressService;
        _loadLevelState = loadLevelState;
        _questPopup = questPopup;
        _questsProvider = questsProvider;
    }

    public void InitializePresenter()
    {
        _questPopup.InitializePopup();
    }


    public void ActivateQuest(Quest quest)
    {
        if (_questPopup.GetInactiveQuestElement(out var questElement))
        {
            questElement.gameObject.SetActive(true);
            questElement.SetQuestText(quest.questName);

            questElement.SetQuestRewards(quest.rewards, quest);
            _questElements.Add(quest, questElement);
        }
    }
}