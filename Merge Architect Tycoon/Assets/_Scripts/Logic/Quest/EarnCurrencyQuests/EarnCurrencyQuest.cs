using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EarnCurrencyQuest", menuName = "StaticData/Quests/EarnCurrencyQuest")]
public class EarnCurrencyQuest : Quest
{
    public int currencyAmount;

    [HideInInspector] public List<Reward> Rewards = new();
    public CoinsReward CoinsReward;

    public override List<Reward> GetRewardList() => Rewards;

    public override List<QuestItem> GetQuestItemsToCreate() => new() { };

    public override void GiveReward(Progress progress)
    {
        foreach (var reward in Rewards)
        {
            reward.GiveReward(progress);
        }
    }

    public override void InitializeRewardsAndItems()
    {
        Rewards.Clear();
        Rewards.Add(CoinsReward);
    }

    public override bool IsCompleted(object value)
    {
        return (int)value >= currencyAmount;
    }
}