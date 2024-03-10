using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialCreateFirstBuildingQuest",
    menuName = "StaticData/Quests/TutorialCreateFirstBuildingQuest")]
public class TutorialCreateFirstBuildingQuest : Quest
{
    public string questText;
    public string buildingName;
    
    [HideInInspector] public List<Reward> Rewards = new();
    public CoinsReward CoinsReward;

    [HideInInspector] public List<QuestItem> QuestItemsToCreate = new();
    public BuildingItem BuildingItem;


    public override List<Reward> GetRewardList() => Rewards;
    public override List<QuestItem> GetQuestItemsToCreate() => QuestItemsToCreate;


    public TutorialCreateFirstBuildingQuest()
    {
        giveQuestCondition = GiveQuestCondition.Tutorial;
        questType = QuestType.Tutorial;
    }

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
        QuestItemsToCreate.Clear();

        QuestItemsToCreate.Add(BuildingItem);
        Rewards.Add(CoinsReward);
    }

    public override bool IsCompleted(object value)
    {
        return true;
    }
}