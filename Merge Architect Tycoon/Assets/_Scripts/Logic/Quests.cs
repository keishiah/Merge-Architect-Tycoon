using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

[Serializable]
public class Quests : ISerializationCallbackReceiver
{
    [SerializeField] private List<string> completedQuests = new();

    public List<string> CompletedQuests => completedQuests;

    public void AddQuestToList(string questId)
    {
        completedQuests.Add(questId);
    }


    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
    }
}