using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MergeCountQuest", menuName = "StaticData/Quests/MergeCountQuest")]
public class MergeCountQuestInfo : BaseQuestInfo
{
    private readonly List<Reward> _rewards = new();
    public CoinsReward coinsReward;

    private readonly List<QuestObjective> _questItemsToCreate = new();

    public override List<Reward> GetRewardList() => _rewards;
    public override List<QuestObjective> GetQuestObjectives() => _questItemsToCreate;

    public MergeCountQuestInfo()
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
        //_questItemsToCreate.Clear();

        //_questItemsToCreate.Add(mergeQuestItem);
        _rewards.Add(coinsReward);
    }

    public void AddMergeItem(MergeCountData mergeCountData)
    {
        mergeCountData.currentMergeCount++;
    }

    public override bool IsCompleted(QuestData questData)
    {
        if (questData is MergeCountData questMergeCountData)
            return _questItemsToCreate[0].GoalCount >= questMergeCountData.currentMergeCount;

        return false;
    }

    public override QuestData GetNewQuestData()
    {
        return new MergeCountData();
    }
}
public class MergeCountData : QuestData
{
    public int currentMergeCount;

    public override void Subscribe(PlayerProgress playerProgress)
    {
        playerProgress.OnMergeItem += () => { currentMergeCount++; };
    }
}