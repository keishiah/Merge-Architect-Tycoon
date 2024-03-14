using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public class Quests : IDisposable
{
    [SerializeField] private List<string> completedQuests = new();
    [SerializeField] private List<string> activeQuests = new();
    [SerializeField] private List<string> questsWaitingForClaim = new();

    public List<string> ActiveQuests => activeQuests;
    public List<string> QuestsWaitingForClaim => questsWaitingForClaim;
    public List<string> CompletedQuests => completedQuests;

    [SerializeField] private int currentMergeCount;
    public int CurrentMergeCount => currentMergeCount;

    [SerializeField] private int currentTruckCount;
    public int CurrentTruckCount => currentTruckCount;
    [SerializeField] private int currentQuestCoinsCount;
    public int CurrentQuestCoinsCount => currentQuestCoinsCount;

    public MergeTierQuests mergeTierQuests = new();

    private ReactiveCommand _onQuestCompleted = new();
    private ReactiveCommand _onQuestValuesChanged = new();

    public void SetProgress(Progress progress)
    {
        progress.Coins.SubscribeToCoinsAdded(AddQuestCoins);
    }

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
        _onQuestCompleted.Execute();
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public void AddMergeItem()
    {
        if (activeQuests.Count <= 1)
            return;
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
        if (activeQuests.Count <= 1)
            return;
        currentTruckCount++;
        SaveLoadService.Save(SaveKey.Quests, this);
        _onQuestValuesChanged.Execute();
    }

    public void ClearTruckCount()
    {
        currentTruckCount = 0;
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public void AddQuestCoins(int coins)
    {
        if (activeQuests.Count <= 1)
            return;

        currentQuestCoinsCount += coins;
        SaveLoadService.Save(SaveKey.Quests, this);
        _onQuestValuesChanged.Execute();
    }

    public void ClearQuestCoinsCount()
    {
        currentQuestCoinsCount = 0;
        SaveLoadService.Save(SaveKey.Quests, this);
    }

    public void AddMergeTierItem(int tier)
    {
        if (activeQuests.Count <= 1)
            return;
        mergeTierQuests.AddTierMergeCount(tier);
        SaveLoadService.Save(SaveKey.Quests, this);
        _onQuestValuesChanged.Execute();
    }

    public IDisposable SubscribeToQuestValueChanged(Action onQuestValueChanged) =>
        _onQuestValuesChanged.Subscribe(_ => onQuestValueChanged());

    public IDisposable SubscribeToQuestCompleted(Action onQuestCompleted) =>
        _onQuestCompleted.Subscribe(_ => onQuestCompleted());


    public void Dispose()
    {
        _onQuestValuesChanged.Dispose();
        _onQuestCompleted.Dispose();
    }
}