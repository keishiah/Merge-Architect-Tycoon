using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "StaticData/QuestInfo", order = 0)]
public class QuestInfo : ScriptableObject
{
    public List<BaseQuestInfo> quests;
}


