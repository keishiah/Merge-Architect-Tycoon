using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

[Serializable]
public class Quests : ISerializationCallbackReceiver
{
    [SerializeField] private List<string> completedQuests = new();
    [SerializeField] private List<string> questsWaitingForClaim = new();

    public List<string> CompletedQuests => completedQuests;
    public List<string> QuestsWaitingForClaim => questsWaitingForClaim;

    public void AddQuestToList(string questId)
    {
        completedQuests.Add(questId);
        if (questsWaitingForClaim.Contains(questId))
            questsWaitingForClaim.Remove(questId);
    }

    public void AddQuestWaitingForClaim(string questId)
    {
        questsWaitingForClaim.Add(questId);
    }


    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
    }
}