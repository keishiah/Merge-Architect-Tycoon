using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public class QuestsData
{
    [SerializeField] public ReactiveCollection<string> CompletedQuests = new();
    [SerializeField] public List<QuestData> ActiveQuests = new();
}