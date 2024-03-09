using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FirstDistrictEarningQuest", menuName = "StaticData/Quests/FirstDistrictEarningQuest")]
public class FirstDistrictEarningQuest : Quest
{
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

    public FirstDistrictEarningQuest()
    {
        questName = "FirstDistrictEarn";
        questType = QuestType.EarnCurrency;
        giveQuestCondition = GiveQuestCondition.Tutorial;
    }

    public override bool IsCompleted(object value)
    {
        return true;
    }
}