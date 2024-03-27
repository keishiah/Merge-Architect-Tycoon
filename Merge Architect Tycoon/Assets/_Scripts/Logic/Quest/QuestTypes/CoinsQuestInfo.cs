using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinsQuest", menuName = "StaticData/Quests/CoinsQuest")]
public class CoinsQuestInfo : BaseQuestInfo
{
    private readonly List<Reward> _rewards = new();
    public CoinsReward coinsReward;

    private readonly List<QuestObjective> _questObjectives = new();
    public CoinsQuestObjective coinsQuestObjective;

    public override List<Reward> GetRewardList() => _rewards;
    public override List<QuestObjective> GetQuestObjectives() => _questObjectives;

    public CoinsQuestInfo()
    {
        GiveQuestCondition = GiveQuestCondition.Base;
    }

    public override void GiveReward(PlayerProgressService progressService)
    {
        foreach (var reward in _rewards)
        {
            reward.GiveReward(progressService);
        }
    }

    public override void InitializeRewardsAndItems()
    {
        _rewards.Clear();
        _questObjectives.Clear();

        _questObjectives.Add(coinsQuestObjective);
        _rewards.Add(coinsReward);
    }


    public override bool IsCompleted(QuestData questData)
    {
        if(questData is CoinsQuestData data)
            return data.currentCoinsCount >= coinsQuestObjective.GoalCount;

        return false;
    }
    public override QuestData GetNewQuestData()
    {
        return new CoinsQuestData();
    }
}
public class CoinsQuestData : QuestData
{
    public int currentCoinsCount;

    public override void Subscribe(PlayerProgress playerProgress)
    {
        
    }
}