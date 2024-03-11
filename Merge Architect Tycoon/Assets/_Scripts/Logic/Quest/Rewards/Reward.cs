using System;
using UnityEngine;
using Zenject;

[Serializable]
public abstract class Reward
{
    public string rewardText;
    public Sprite rewardSprite;
    public int rewardAmount;


    public abstract void GiveReward(Progress progress);
}