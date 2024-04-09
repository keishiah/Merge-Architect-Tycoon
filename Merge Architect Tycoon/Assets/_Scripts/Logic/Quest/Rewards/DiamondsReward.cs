using UnityEngine;

[CreateAssetMenu(fileName = "DiamondsReward",
    menuName = "StaticData/Quests/Rewards/DiamondsReward")]
public class DiamondsReward : Reward
{
    public override void GiveReward(PlayerProgressService progress)
    {
        progress.AddDiamonds(Amount);
    }
}