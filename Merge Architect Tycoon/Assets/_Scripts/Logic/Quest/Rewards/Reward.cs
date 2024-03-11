using System;
using UnityEngine;
using Zenject;

[Serializable]
public abstract class Reward
{
    public string rewardName;
    public Sprite rewardSprite;
    public int rewardAmount;


    public abstract void GiveReward(Progress progress);
}