using UnityEngine;

[CreateAssetMenu(fileName = "CreateBuildingQuest", menuName = "StaticData/CreateBuildingQuest")]
public class CreateBuildingQuest : Quest
{
    public string buildingName;
    public Sprite buildingImage;
    public CoinsReward CoinsReward;
    

    public override void GiveReward() => CoinsReward.GiveReward();

    public virtual bool IsCompleted(string buildingName)
    {
        return this.buildingName == buildingName;
    }
}