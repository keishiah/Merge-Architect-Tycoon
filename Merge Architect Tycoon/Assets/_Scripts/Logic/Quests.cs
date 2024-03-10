using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

[Serializable]
public class Quests : ISerializationCallbackReceiver
{
    [SerializeField] private List<string> completedQuests = new();
    [SerializeField] private List<string> activeQuests = new();
    [SerializeField] private List<string> questsWaitingForClaim = new();

    public List<string> CompletedQuests => completedQuests;
    public List<string> ActiveQuests => activeQuests;
    public List<string> QuestsWaitingForClaim => questsWaitingForClaim;

    public void AddActiveQuest(string questId)
    {
        activeQuests.Add(questId);
    }

    public void AddQuestWaitingForClaim(string questId)
    {
        questsWaitingForClaim.Add(questId);
        if (activeQuests.Contains(questId))
            activeQuests.Remove(questId);
    }

    public void AddCompletedQuest(string questId)
    {
        completedQuests.Add(questId);
        if (questsWaitingForClaim.Contains(questId))
            questsWaitingForClaim.Remove(questId);
    }


    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
    }
}