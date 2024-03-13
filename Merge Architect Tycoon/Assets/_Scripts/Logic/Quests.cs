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

    [SerializeField] private int currentMergeCount;
    public int CurrentMergeCount => currentMergeCount;

    [SerializeField] private int currentTruckCount;
    public int CurrentTruckCount => currentTruckCount;

    private ReactiveCommand _onQuestValuesChanged = new();
    private ReactiveCommand _onTruckValuesChanged = new();
    private ReactiveCommand _onQuestCompleted = new();

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
        _onQuestCompleted.Execute();
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

    public void AddTruckItem()
    {
        currentTruckCount++;
        SaveLoadService.Save(SaveKey.Quests, this);
        _onTruckValuesChanged.Execute();
    }

    public void ClearTruckCount()
    {
        currentTruckCount = 0;
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public IDisposable SubscribeToMerge(Action onQuestValueChanged) =>
        _onQuestValuesChanged.Subscribe(_ => onQuestValueChanged());

    public IDisposable SubscribeToQuestCompleted(Action onQuestCompleted) =>
        _onQuestCompleted.Subscribe(_ => onQuestCompleted());

    public IDisposable SubscribeToTruckValueChanged(Action onTruckValueChanged) =>
        _onTruckValuesChanged.Subscribe(_ => onTruckValueChanged());

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
    }

    public void Dispose()
    {
        _onQuestValuesChanged.Dispose();
        _onQuestCompleted.Dispose();
        _onTruckValuesChanged.Dispose();        
    }
}