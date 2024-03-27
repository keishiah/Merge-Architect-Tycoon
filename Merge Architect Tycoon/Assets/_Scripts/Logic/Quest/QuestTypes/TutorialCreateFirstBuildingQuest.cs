using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialCreateFirstBuildingQuest",
    menuName = "StaticData/Quests/TutorialCreateFirstBuildingQuest")]
public class TutorialCreateFirstBuildingQuest : BaseQuestInfo
{
    private readonly List<Reward> _rewards = new();
    public CoinsReward coinsReward;

    private readonly List<QuestObjective> _questItemsToCreate = new();
    public BuildingObjective buildingItem;

    public override List<Reward> GetRewardList() => _rewards;
    public override List<QuestObjective> GetQuestObjectives() => _questItemsToCreate;

    public TutorialCreateFirstBuildingQuest()
    {
        GiveQuestCondition = GiveQuestCondition.Tutorial;
    }

    public override void GiveReward(PlayerProgressService progressService)
    {
        foreach (var reward in _rewards)
        {
            reward.GiveReward(progressService);
        }
    }

    public override void InitializeRewardsAndItems()
    {
        _rewards.Clear();
        _questItemsToCreate.Clear();

        _questItemsToCreate.Add(buildingItem);
        _rewards.Add(coinsReward);
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