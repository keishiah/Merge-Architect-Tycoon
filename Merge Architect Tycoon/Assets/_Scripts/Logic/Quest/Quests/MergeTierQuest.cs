using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MergeTierQuest",
    menuName = "StaticData/Quests/MergeTierQuest")]
public class MergeTierQuest : Quest
{
    private readonly List<Reward> _rewards = new();
    public CoinsReward coinsReward;

    private readonly List<QuestItem> _questItemsToCreate = new();
    public MergeTierQuestItem mergeTierQuestItem;


    public override List<Reward> GetRewardList() => _rewards;
    public override List<QuestItem> GetQuestItemsToCreate() => _questItemsToCreate;


    public MergeTierQuest()
    {
        giveQuestCondition = GiveQuestCondition.Base;
    }

    public override void GiveReward(IPlayerProgressService progress)
    {
        foreach (var reward in _rewards)
        {
            reward.GiveReward(progress.Progress);
        }

        progress.Quests.ClearMergeCount();
    }

    public override void InitializeRewardsAndItems()
    {
        _rewards.Clear();
        _questItemsToCreate.Clear();

        _questItemsToCreate.Add(mergeTierQuestItem);
        _rewards.Add(coinsReward);
    }


    public override bool IsCompleted(IPlayerProgressService progress)
    {
        return progress.Quests.mergeTierQuests.GetCurrentItemCount(mergeTierQuestItem.ItemLevel) >=
               mergeTierQuestItem.itemCount;
    }
}