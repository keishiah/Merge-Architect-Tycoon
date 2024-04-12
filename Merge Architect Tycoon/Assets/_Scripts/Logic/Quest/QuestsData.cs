using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public class QuestsData
{
    [SerializeField] public ReactiveProperty<string> LastCompletedQuest = new();
    [SerializeField] public List<string> CompletedQuests = new();
    [SerializeField] public List<QuestData> ActiveQuests = new();
}