using System;
using UnityEngine;
using Zenject;

[Serializable]
public class Reward
{
    public string rewardName;
    public Sprite rewardSprite;
    public int rewardAmount;

    [Inject] protected IPlayerProgressService PlayerProgressService;

    public virtual void GiveReward()
    {
    }
}

[Serializable]
public class CoinsReward : Reward
{
    public CoinsReward()
    {
        rewardName = "Coins";
        rewardAmount = 100;
    }

    public override void GiveReward()
    {
        PlayerProgressService.Progress.AddCoins(rewardAmount);
    }
}