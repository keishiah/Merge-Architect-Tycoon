using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Quest : ScriptableObject
{
    [HideInInspector] public string questId;

    public string questName;
    public abstract void GiveReward();
    public List<Reward> rewards;


    //
    // public List<MergeItem> itemsToMerge;
    // public List<int> itemsCount;

    protected Quest()
    {
        questId = Guid.NewGuid().ToString();
    }
}