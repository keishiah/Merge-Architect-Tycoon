using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;


public class QuestsProvider : IInitializableOnSceneLoaded
{
    private List<Quest> _activeQuests = new();
    private IStaticDataService _staticDataService;

    [Inject]
    void Construct(IStaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
    }

    public void OnSceneLoaded()
    {
    }

    public Quest GetFirstQuest()
    {
        return _staticDataService.Quests.Values.ToList()[0];
    }

    public void ActivateQuest(Quest questValue)
    {
        _activeQuests.Add(questValue);
    }

    public void SubscribeQuestProgress(Quest quest)
    {
        
    }
}