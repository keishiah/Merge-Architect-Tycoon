using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinsQuest",
    menuName = "StaticData/Quests/CoinsQuest")]
public class CoinsQuest : Quest
{
    private readonly List<Reward> _rewards = new();
    public CoinsReward coinsReward;

    private readonly List<QuestItem> _questItemsToCreate = new();
    public CoinsQuestItem coinsQuestItem;


    public override List<Reward> GetRewardList() => _rewards;
    public override List<QuestItem> GetQuestItemsToCreate() => _questItemsToCreate;


    public CoinsQuest()
    {
        giveQuestCondition = GiveQuestCondition.Base;
    }

    public override void GiveReward(IPlayerProgressService progress)
    {
        foreach (var reward in _rewards)
        {
            reward.GiveReward(progress.Progress);
        }

        progress.Quests.ClearQuestCoinsCount();
    }

    public override void InitializeRewardsAndItems()
    {
        _rewards.Clear();
        _questItemsToCreate.Clear();

        _questItemsToCreate.Add(coinsQuestItem);
        _rewards.Add(coinsReward);
    }


    public override bool IsCompleted(IPlayerProgressService progress)
    {
        return progress.Quests.CurrentQuestCoinsCount >= coinsQuestItem.itemCount;
    }
}