using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestsData
{
    [SerializeField] public List<string> CompletedQuests = new();
    [SerializeField] public List<QuestData> ActiveQuests = new();
}