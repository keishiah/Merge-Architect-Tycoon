using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TruckQuest",
    menuName = "StaticData/Quests/TruckQuest")]
public class TruckQuestInfo : BaseQuestInfo
{
    private readonly List<Reward> _rewards = new();
    public CoinsReward coinsReward;

    private readonly List<QuestObjective> _questItemsToCreate = new();
    public TruckQuestObjective truckQuestItem;

    public override List<Reward> GetRewardList() => _rewards;
    public override List<QuestObjective> GetQuestObjectives() => _questItemsToCreate;


    public TruckQuestInfo()
    {
        GiveQuestCondition = GiveQuestCondition.Base;
    }

    public override void GiveReward(PlayerProgressService progressService)
    {
        foreach (var reward in _rewards)
        {
            reward.GiveReward(progressService);
        }

        //progress.Quests.ClearTruckCount();
    }

    public override void InitializeRewardsAndItems()
    {
        _rewards.Clear();
        _questItemsToCreate.Clear();

        _questItemsToCreate.Add(truckQuestItem);
        _rewards.Add(coinsReward);
    }


    public override bool IsCompleted(QuestData questData)
    {
        //return progress.Quests.CurrentTruckCount >= truckQuestItem.GoalCount;
        if (questData is TruckQuestData data)
            return data.currentTruckCount >= truckQuestItem.GoalCount;

        return false;
    }
    public override QuestData GetNewQuestData()
    {
        return new TruckQuestData();
    }
}
public class TruckQuestData : QuestData
{
    public int currentTruckCount;

    public override void Subscribe(PlayerProgress playerProgress)
    {
        
    }
}