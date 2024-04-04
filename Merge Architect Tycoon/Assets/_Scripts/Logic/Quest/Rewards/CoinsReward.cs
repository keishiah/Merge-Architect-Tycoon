using UnityEngine;

[CreateAssetMenu(fileName = "CoinsReward",
    menuName = "StaticData/Quests/Rewards/CoinsReward")]
public class CoinsReward : Reward
{
    public override void GiveReward(PlayerProgressService progress)
    {
        progress.AddCoins(rewardAmount);
    }
}
