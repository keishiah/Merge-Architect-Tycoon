using UnityEngine;

[CreateAssetMenu(fileName = "CreateBuildingQuest", menuName = "StaticData/CreateBuildingQuest")]
public class CreateBuildingQuest : Quest
{
    public string buildingName;
    public Sprite buildingImage;


    public bool IsCompleted(string buildingName)
    {
        return this.buildingName == buildingName;
    }


    public override void GiveReward()
    {
        foreach (var reward in rewards)
        {
            reward.GiveReward();
        }
    }
}