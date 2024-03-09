using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateBuildingQuest", menuName = "StaticData/CreateBuildingQuest")]
public class CreateBuildingQuest : Quest
{
    public string buildingName;
    public Sprite buildingImage;
    public List<Reward> Rewards;

    public List<QuestItem> QuestItemsToCreate = new();
    public BuildingItem BuildingItem;


    public override void GiveReward(Progress progress)
    {
        foreach (var reward in Rewards)
        {
            reward.GiveReward(progress);
        }
    }

    public override List<Reward> GetRewardList() => Rewards;
    public override List<QuestItem> GetQuestItemsToCreate() => new List<QuestItem>(){BuildingItem};
    public override void InitializeRewardsAndItems()
    {
        QuestItemsToCreate.Add(BuildingItem);
    }

    public override bool IsCompleted<T>(T value)
    {
        return value.ToString() == buildingName;
    }
}