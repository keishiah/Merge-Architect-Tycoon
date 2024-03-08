using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateBuildingQuest", menuName = "StaticData/CreateBuildingQuest")]
public class CreateBuildingQuest : Quest
{
    public string buildingName;
    public Sprite buildingImage;
    public CoinsReward CoinsReward;
    public List<QuestItem> QuestItemsToCreate;


    public override void GiveReward() => CoinsReward.GiveReward();

    public override List<Reward> GetRewardList() => new() { CoinsReward };

    public override List<QuestItem> GetQuestItemsToCreate() => QuestItemsToCreate;

    // public override Dictionary<object, int> GetQuestItemsToCreateDictionary() => new() { { buildingName, 1 } };

    public override bool IsCompleted<T>(T value)
    {
        if (IsCompleted(value as string)) return true;
        return false;
    }


    public virtual bool IsCompleted(string buildingName) => this.buildingName == buildingName;
}