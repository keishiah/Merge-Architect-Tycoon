using System;
using UnityEngine;
using Zenject;

[Serializable]
public class Reward
{
    public string rewardName;
    public Sprite rewardSprite;
    public int rewardAmount;


    public virtual void GiveReward(Progress progress)
    {
    }
}

[Serializable]
public class CoinsReward : Reward
{
    public override void GiveReward(Progress progress)
    {
        progress.AddCoins(rewardAmount);
    }
}