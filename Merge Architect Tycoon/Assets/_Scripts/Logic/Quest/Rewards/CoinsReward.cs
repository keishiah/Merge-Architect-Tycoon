using System;
using UnityEngine;

[Serializable]
public class CoinsReward : Reward
{
    public override void GiveReward(Progress progress)
    {
        progress.AddCoins(rewardAmount);
    }
}