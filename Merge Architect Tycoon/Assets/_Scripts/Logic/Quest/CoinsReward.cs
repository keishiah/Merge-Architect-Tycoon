using UnityEngine;

[CreateAssetMenu(fileName = "CoinsReward", menuName = "StaticData/Rewards/CoinsReward")]
public class CoinsReward : Reward
{
    public override void GiveReward(Progress progress)
    {
        progress.AddCoins(rewardAmount);
    }
}