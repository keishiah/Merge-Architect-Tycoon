using System;

[Serializable]
public class CoinsReward : Reward
{
    public override void GiveReward(PlayerProgressService progress)
    {
        progress.AddCoins(rewardAmount);
    }
}