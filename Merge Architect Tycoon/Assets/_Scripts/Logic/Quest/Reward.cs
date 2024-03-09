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