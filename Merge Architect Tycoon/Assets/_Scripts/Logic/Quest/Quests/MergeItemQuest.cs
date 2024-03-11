using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MergeItemQuest",
    menuName = "StaticData/Quests/MergeItemQuest")]
public class MergeItemQuest : Quest
{
    public string questText;

    private readonly List<Reward> _rewards = new();
    public CoinsReward coinsReward;

    private readonly List<QuestItem> _questItemsToCreate = new();
    public MergeQuestItem mergeQuestItem;


    public override List<Reward> GetRewardList() => _rewards;
    public override List<QuestItem> GetQuestItemsToCreate() => _questItemsToCreate;


    public MergeItemQuest()
    {
        giveQuestCondition = GiveQuestCondition.Merge;
    }

    public override void GiveReward(Progress progress)
    {
        foreach (var reward in _rewards)
        {
            reward.GiveReward(progress);
        }
    }

    public override void InitializeRewardsAndItems()
    {
        _rewards.Clear();
        _questItemsToCreate.Clear();

        _questItemsToCreate.Add(mergeQuestItem);
        _rewards.Add(coinsReward);
    }

    public override bool IsCompleted(object value)
    {
        return (int)value >= mergeQuestItem.itemCount;
    }
}