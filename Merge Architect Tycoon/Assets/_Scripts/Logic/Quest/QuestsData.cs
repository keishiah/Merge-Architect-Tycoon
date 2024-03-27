using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestsData// : IDisposable
{
    [SerializeField] public List<string> CompletedQuests = new();
    [SerializeField] public List<QuestData> ActiveQuests = new();
    [SerializeField] public List<string> QuestsWaitingForClaim = new();

    //private ReactiveCommand _onQuestCompleted = new();
    //private ReactiveCommand _onQuestValuesChanged = new();

    //public void SetProgress(PlayerProgress progress)
    //{
    //    progress.Riches.Coins.Subscribe(AddQuestCoins);
    //}

    //public void ClearMergeCount()
    //{
    //    currentMergeCount = 0;
    //    SaveLoadService.Save(SaveKey.Quests, this);
    //}

    //public void AddTruckItem()
    //{
    //    if (activeQuests.Count <= 1)
    //        return;
    //    currentTruckCount++;
    //    SaveLoadService.Save(SaveKey.Quests, this);
    //    _onQuestValuesChanged.Execute();
    //}

    //public void ClearTruckCount()
    //{
    //    currentTruckCount = 0;
    //    SaveLoadService.Save(SaveKey.Quests, this);
    //}

    //public void AddQuestCoins(int coins)
    //{
    //    if (activeQuests.Count <= 1)
    //        return;

    //    currentQuestCoinsCount += coins;
    //    SaveLoadService.Save(SaveKey.Quests, this);
    //    _onQuestValuesChanged.Execute();
    //}

    //public void ClearQuestCoinsCount()
    //{
    //    currentQuestCoinsCount = 0;
    //    SaveLoadService.Save(SaveKey.Quests, this);
    //}

    //public void AddMergeTierItem(int tier)
    //{
    //    if (activeQuests.Count <= 1)
    //        return;
    //    mergeTierQuests.AddTierMergeCount(tier);
    //    SaveLoadService.Save(SaveKey.Quests, this);
    //    _onQuestValuesChanged.Execute();
    //}


    //public void Dispose()
    //{
    //    _onQuestValuesChanged.Dispose();
    //    _onQuestCompleted.Dispose();
    //}
}