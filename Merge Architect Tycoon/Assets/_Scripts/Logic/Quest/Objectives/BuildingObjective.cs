using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateBuildingObjective",
    menuName = "StaticData/Quests/Objectives/CreateBuildingObjective")]
public class BuildingObjective : QuestObjective
{
    public string targetBuildingName;

    public override string GetDescription()
    {
        return "Build " + targetBuildingName;
    }
    public override string GetProgressText(QuestProgress questProgress)
    {
        return $"{questProgress.Numeral}/1";
    }
    public override void DoSubscribe(PlayerProgress playerProgress, QuestProgress questProgress)
    {
        System.IDisposable subscription = playerProgress.Buldings.CreatedBuildings.ObserveAdd()
            .Where(x => x.Value == targetBuildingName)
            .Subscribe(x => QuestDone(questProgress));

        questProgress.Subscription = subscription;
    }

    private void QuestDone(QuestProgress questProgress)
    {
        questProgress.Numeral = 1;
        questProgress.IsComplete = true;
    }

    public override bool IsComplete(PlayerProgress playerData, QuestProgress questProgress = null)
    {
        if(questProgress != null && questProgress.IsComplete)
            return true;

        return playerData.Buldings.CreatedBuildings.Contains(targetBuildingName);
    }
}