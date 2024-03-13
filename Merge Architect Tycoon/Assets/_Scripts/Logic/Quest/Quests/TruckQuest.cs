using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TruckQuest",
    menuName = "StaticData/Quests/TruckQuest")]
public class TruckQuest : Quest
{
    private readonly List<Reward> _rewards = new();
    public CoinsReward coinsReward;

    private readonly List<QuestItem> _questItemsToCreate = new();
    public TruckQuestItem truckQuestItem;


    public override List<Reward> GetRewardList() => _rewards;
    public override List<QuestItem> GetQuestItemsToCreate() => _questItemsToCreate;


    public TruckQuest()
    {
        giveQuestCondition = GiveQuestCondition.Base;
    }

    public override void GiveReward(IPlayerProgressService progress)
    {
        foreach (var reward in _rewards)
        {
            reward.GiveReward(progress.Progress);
        }

        progress.Quests.ClearTruckCount();
    }

    public override void InitializeRewardsAndItems()
    {
        _rewards.Clear();
        _questItemsToCreate.Clear();

        _questItemsToCreate.Add(truckQuestItem);
        _rewards.Add(coinsReward);
    }


    public override bool IsCompleted(IPlayerProgressService progress)
    {
        Debug.Log(progress.Quests.CurrentTruckCount);
        Debug.Log(truckQuestItem.itemCount);
        return progress.Quests.CurrentTruckCount >= truckQuestItem.itemCount;
    }
}