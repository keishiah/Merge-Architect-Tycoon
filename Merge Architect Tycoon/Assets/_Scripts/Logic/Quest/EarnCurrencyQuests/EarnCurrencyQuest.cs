using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EarnCurrencyQuest", menuName = "StaticData/EarnCurrencyQuest")]
public class EarnCurrencyQuest : Quest
{
    public string buildingName;
    public Sprite buildingImage;

    [HideInInspector] public List<Reward> Rewards = new();
    public CoinsReward CoinsReward;
    public CoinsReward CoinsReward1;

    [HideInInspector] public List<QuestItem> QuestItemsToCreate = new();
    public BuildingItem BuildingItem;


    public override List<Reward> GetRewardList() => Rewards;

    public override void GiveReward(Progress progress)
    {
        foreach (var reward in Rewards)
        {
            reward.GiveReward(progress);
        }
    }

    public override List<QuestItem> GetQuestItemsToCreate() => new List<QuestItem>() { BuildingItem };

    public override void InitializeRewardsAndItems()
    {
        Rewards.Clear();
        QuestItemsToCreate.Add(BuildingItem);
        Rewards.Add(CoinsReward);
        Rewards.Add(CoinsReward1);
    }

    public override bool IsCompleted<T>(T value)
    {
        return value.ToString() == buildingName;
    }
}