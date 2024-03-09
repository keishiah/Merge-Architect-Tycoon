using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateBuildingQuest", menuName = "StaticData/CreateBuildingQuest")]
public class CreateBuildingQuest : Quest
{
    public string buildingName;
    public Sprite buildingImage;
    public CoinsReward CoinsReward;
    public List<QuestItem> QuestItemsToCreate;

    [HideInInspector] public List<Reward> RewardList = new();

    public CreateBuildingQuest()
    {
        RewardList.Add(CoinsReward);
    }

    public override void GiveReward(Progress progress) => CoinsReward.GiveReward(progress);
    public override List<Reward> GetRewardList() => RewardList;
    public override List<QuestItem> GetQuestItemsToCreate() => QuestItemsToCreate;

    public override bool IsCompleted<T>(T value)
    {
        return value.ToString() == buildingName;
    }
}