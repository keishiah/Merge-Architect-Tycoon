using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

[Serializable]
public class Quests : ISerializationCallbackReceiver, IDisposable
{
    [SerializeField] private ReactiveCollection<int> completedQuests = new();
    [SerializeField] private List<int> activeQuests = new();
    [SerializeField] private List<int> questsWaitingForClaim = new();

    public List<int> ActiveQuests => activeQuests;
    public List<int> QuestsWaitingForClaim => questsWaitingForClaim;
    public IReadOnlyCollection<int> CompletedQuests => completedQuests;

    [SerializeField] private int currentMergeCount;
    public int CurrentMergeCount => currentMergeCount;

    private ReactiveCommand _onQuestValuesChanged = new();

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
        currentMergeCount++;
        SaveLoadService.Save(SaveKey.Quests, this);
        _onQuestValuesChanged.Execute();
    }

    public void ClearMergeCount()
    {
        currentMergeCount = 0;
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public IDisposable SubscribeToQuestCompletion(Action onQuestCompletion) =>
        completedQuests.ObserveAdd().Subscribe(_ => onQuestCompletion());

    public IDisposable SubscribeToMerge(Action onQuestValueChanged) =>
        _onQuestValuesChanged.Subscribe(_ => onQuestValueChanged());

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
    }

    public void Dispose()
    {
        _onQuestValuesChanged.Dispose();
    }
}