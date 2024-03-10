using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialDistrictEarningQuest",
    menuName = "StaticData/Quests/TutorialDistrictEarningQuest")]
public class TutorialDistrictEarningQuest : Quest
{
    [HideInInspector] public List<Reward> Rewards = new();
    public CoinsReward CoinsReward;

    public override List<Reward> GetRewardList() => Rewards;
    public override List<QuestItem> GetQuestItemsToCreate() => new();

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

    public TutorialDistrictEarningQuest()
    {
        questName = "FirstDistrictEarn";
        giveQuestCondition = GiveQuestCondition.Tutorial;
        questType = QuestType.Tutorial;
    }

    public override bool IsCompleted(object value)
    {
        return true;
    }
}