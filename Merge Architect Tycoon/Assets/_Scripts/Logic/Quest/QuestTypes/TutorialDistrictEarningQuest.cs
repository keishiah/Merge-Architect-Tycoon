using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialDistrictEarningQuest",
    menuName = "StaticData/Quests/TutorialDistrictEarningQuest")]
public class TutorialDistrictEarningQuest : BaseQuestInfo
{
    [HideInInspector] public List<Reward> Rewards = new();
    public CoinsReward CoinsReward;

    public override List<Reward> GetRewardList() => Rewards;
    public override List<QuestObjective> GetQuestObjectives() => new();

    public override void GiveReward(PlayerProgressService progressService)
    {
        foreach (var reward in Rewards)
        {
            reward.GiveReward(progressService);
        }
    }

    public override void InitializeRewardsAndItems()
    {
        Rewards.Clear();
        Rewards.Add(CoinsReward);
    }

    public TutorialDistrictEarningQuest()
    {
        GiveQuestCondition = GiveQuestCondition.Tutorial;
    }

    public override bool IsCompleted(QuestData questData)
    {
        return true;
    }
    public override QuestData GetNewQuestData()
    {
        return null;
    }
}