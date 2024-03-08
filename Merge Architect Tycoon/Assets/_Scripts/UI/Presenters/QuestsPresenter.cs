using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestsPresenter
{
    private IPlayerProgressService _playerProgressService;

    private QuestPopup _questPopup;
    private Dictionary<Quest, QuestElement> _questElements = new();

    private QuestsProvider _questsProvider;

    [Inject]
    void Construct(IPlayerProgressService playerProgressService, QuestPopup questPopup,
        QuestsProvider questsProvider)
    {
        _playerProgressService = playerProgressService;
        _questPopup = questPopup;
        _questsProvider = questsProvider;
    }

    public void InitializePresenter()
    {
        _questPopup.InitializePopup();
    }

    public void ActivateQuest(Quest quest)
    {
        // if (_questPopup.GetInactiveQuestElement(out var questElement))
        // {
        //     questElement.gameObject.SetActive(true);
        //     questElement.SetQuestText(quest.questName);
        //     _questElements.Add(quest, questElement);
        //
        //     switch (quest.questType)
        //     {
        //         case QuestType.BuildingQuest:
        //             questElement.ActivateBuildingQuest(quest.rewards, quest);
        //             break;
        //         case QuestType.CreateItemQuest:
        //             questElement.ActivateMergeQuest(quest.rewards, quest);
        //             break;
        //     }
        // }
    }

    public void CompleteBuildingQuest(Quest quest)
    {
        _questElements.TryGetValue(quest, out var questElement);
        questElement.SetQuestAsCompleted();
    }
    
    
    
}