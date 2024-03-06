using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "StaticData/QuestData", order = 0)]
public class QuestData : ScriptableObject
{
    public List<Quest> quests;
}

public enum QuestType
{
    CreateItemQuest,
    BuildingQuest
}
