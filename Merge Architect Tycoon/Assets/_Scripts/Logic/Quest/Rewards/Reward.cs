using System;
using UnityEngine;

[Serializable]
public abstract class Reward
{
    public Sprite rewardSprite;
    public int rewardAmount;

    public abstract void GiveReward(PlayerProgressService progress);
}