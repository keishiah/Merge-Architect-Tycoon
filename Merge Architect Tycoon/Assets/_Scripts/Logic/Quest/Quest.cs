using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Quest", menuName = "StaticData/Quest", order = 0)]
public class Quest : ScriptableObject
{
    [HideInInspector] public string questId;
    public QuestType questType;
    public string questName;
    public List<Reward> rewards;

    public string buildingName;
    public Sprite buildingImage;
    
    public List<MergeItem> itemsToMerge;
    public List<int> itemsCount;


    public Quest()
    {
        questId = Guid.NewGuid().ToString();
    }
}

[Serializable]
public class Reward
{
    public string rewardName;
    public int rewardAmount;
    public Sprite rewardSprite;
}