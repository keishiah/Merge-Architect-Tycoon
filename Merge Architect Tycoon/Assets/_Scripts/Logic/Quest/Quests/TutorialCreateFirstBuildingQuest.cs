using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialCreateFirstBuildingQuest",
    menuName = "StaticData/Quests/TutorialCreateFirstBuildingQuest")]
public class TutorialCreateFirstBuildingQuest : QuestBase
{
    private readonly List<Reward> _rewards = new();
    public CoinsReward coinsReward;

    private readonly List<QuestItem> _questItemsToCreate = new();
    public BuildingItem buildingItem;


    public override List<Reward> GetRewardList() => _rewards;
    public override List<QuestItem> GetQuestItemsToCreate() => _questItemsToCreate;

    public TutorialCreateFirstBuildingQuest()
    {
        giveQuestCondition = GiveQuestCondition.Tutorial;
    }

    public override void GiveReward(IPlayerProgressService progress)
    {
        foreach (var reward in _rewards)
        {
            reward.GiveReward(progress.Progress);
        }
    }

    public override void InitializeRewardsAndItems()
    {
        _rewards.Clear();
        _questItemsToCreate.Clear();

        _questItemsToCreate.Add(buildingItem);
        _rewards.Add(coinsReward);
    }

    public override bool IsCompleted(IPlayerProgressService progress)
    {
        return true;
    }
}