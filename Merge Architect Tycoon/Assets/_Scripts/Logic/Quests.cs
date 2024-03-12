using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

[Serializable]
public class Quests : ISerializationCallbackReceiver, IDisposable
{
    [SerializeField] private List<int> completedQuests = new();
    [SerializeField] private List<int> activeQuests = new();
    [SerializeField] private List<int> questsWaitingForClaim = new();

    public List<int> ActiveQuests => activeQuests;
    public List<int> QuestsWaitingForClaim => questsWaitingForClaim;
    public List<int> CompletedQuests => completedQuests;

    [SerializeField] private ReactiveProperty<int> currentMergeCount = new();
    public int CurrentMergeCount => currentMergeCount.Value;

    public void AddActiveQuest(int questId)
    {
        activeQuests.Add(questId);
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public void AddQuestWaitingForClaim(int questId)
    {
        questsWaitingForClaim.Add(questId);
        if (activeQuests.Contains(questId))
            activeQuests.Remove(questId);
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public void AddCompletedQuest(int questId)
    {
        completedQuests.Add(questId);
        if (questsWaitingForClaim.Contains(questId))
            questsWaitingForClaim.Remove(questId);
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public void AddMergeItem()
    {
        Debug.Log("merge");
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