using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;


public class QuestsProvider : IInitializableOnSceneLoaded
{
    private IStaticDataService _staticDataService;
    private List<Quest> _currentQuests = new();

    [Inject]
    void Construct(IStaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
    }

    public void OnSceneLoaded()
    {
    }

    public Quest GetQuest(string questName)
    {
        _staticDataService.Quests.TryGetValue(questName, out var quest);
        return quest;
    }

    public Quest GetFirstQuest()
    {
        return _staticDataService.Quests.Values.ToList()[0];
    }

    public Quest GetSecondQuest()
    {
        return _staticDataService.Quests.Values.ToList()[1];
    }
}