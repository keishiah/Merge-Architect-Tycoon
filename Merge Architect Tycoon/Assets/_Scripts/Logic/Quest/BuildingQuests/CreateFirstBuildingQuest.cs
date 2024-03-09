using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateBuildingQuest", menuName = "StaticData/Quests/CreateFirstBuildingQuest")]
public class CreateFirstBuildingQuest : Quest
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

    public CreateFirstBuildingQuest()
    {
        questType = QuestType.Building;
        giveQuestCondition = GiveQuestCondition.Tutorial;
    }

    public override List<QuestItem> GetQuestItemsToCreate() => new() {  };

    public override void InitializeRewardsAndItems()
    {
        Rewards.Clear();
        QuestItemsToCreate.Add(BuildingItem);
        Rewards.Add(CoinsReward);
        Rewards.Add(CoinsReward1);
    }

    public override bool IsCompleted(object value)
    {
        return value.ToString() == buildingName;
    }
}