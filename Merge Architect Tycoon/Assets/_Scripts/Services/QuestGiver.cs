using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestGiver : IInitializableOnSceneLoaded
{
    private Dictionary<GiveQuestCondition, Quest> _questByCondition;

    private QuestsProvider _questsProvider;
    private IStaticDataService _staticDataService;

    [Inject]
    void Construct(QuestsProvider questsProvider, IStaticDataService staticDataService)
    {
        _questsProvider = questsProvider;
        _staticDataService = staticDataService;
    }

    public void OnSceneLoaded()
    {
        foreach (var quest in _staticDataService.Quests)
        {
            GiveQuestCondition questCondition = quest.Value.giveQuestCondition;
            _questByCondition.Add(questCondition, quest.Value);
            
            if(questCondition== GiveQuestCondition.Start)
                ActivateQuest(quest.Value);
        }
    }

    private void ActivateQuest(Quest questValue)
    {
        _questsProvider.ActivateQuest(questValue);
    }
}