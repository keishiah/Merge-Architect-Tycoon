using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

[Serializable]
public class Quests : ISerializationCallbackReceiver, IDisposable
{
    [SerializeField] private List<string> completedQuests = new();
    [SerializeField] private List<string> activeQuests = new();
    [SerializeField] private List<string> questsWaitingForClaim = new();

    public List<string> ActiveQuests => activeQuests;
    public List<string> QuestsWaitingForClaim => questsWaitingForClaim;
    public List<string> CompletedQuests => completedQuests;

    [SerializeField] private ReactiveProperty<int> currentMergeCount = new();
    public int CurrentMergeCount => currentMergeCount.Value;

    public void AddActiveQuest(string questId)
    {
        activeQuests.Add(questId);
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public void AddQuestWaitingForClaim(string questId)
    {
        questsWaitingForClaim.Add(questId);
        if (activeQuests.Contains(questId))
            activeQuests.Remove(questId);
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public void AddCompletedQuest(string questId)
    {
        completedQuests.Add(questId);
        if (questsWaitingForClaim.Contains(questId))
            questsWaitingForClaim.Remove(questId);
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public void AddMergeItem()
    {
        currentMergeCount.Value++;
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public void ClearMergeCount()
    {
        currentMergeCount.Value = 0;
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public IDisposable SubscribeToMerge(Action<int> onCoinsCountChanged) =>
        currentMergeCount.Subscribe(onCoinsCountChanged);

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
    }

    public void Dispose()
    {
        currentMergeCount?.Dispose();
    }
}