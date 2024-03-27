using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MergeTierQuest",
    menuName = "StaticData/Quests/MergeTierQuest")]
public class MergeTierQuestInfo : BaseQuestInfo
{
    private readonly List<Reward> _rewards = new();
    public CoinsReward coinsReward;

    private readonly List<QuestObjective> _questItemsToCreate = new();
    public MergeTierQuestObjective mergeTierQuestItem;

    public override List<Reward> GetRewardList() => _rewards;
    public override List<QuestObjective> GetQuestObjectives() => _questItemsToCreate;


    public MergeTierQuestInfo()
    {
        GiveQuestCondition = GiveQuestCondition.Base;
    }

    public override void GiveReward(PlayerProgressService progressService)
    {
        foreach (var reward in _rewards)
        {
            reward.GiveReward(progressService);
        }

        //progress.Quests.ClearMergeCount();
    }

    public override void InitializeRewardsAndItems()
    {
        _rewards.Clear();
        _questItemsToCreate.Clear();

        _questItemsToCreate.Add(mergeTierQuestItem);
        _rewards.Add(coinsReward);
    }


    public override bool IsCompleted(QuestData questData)
    {
        //return progress.Quests.mergeTierQuests.GetCurrentItemCount(mergeTierQuestItem.ItemLevel) >=
        //       mergeTierQuestItem.GoalCount;
        if (questData is MergeTierQuestData data)
            return data.currentMergeTier >= mergeTierQuestItem.GoalCount;

        return false;
    }
    public override QuestData GetNewQuestData()
    {
        return new MergeTierQuestData();
    }
}
public class MergeTierQuestData : QuestData
{
    public int currentMergeTier;

    public override void Subscribe(PlayerProgress playerProgress)
    {
        
    }
}