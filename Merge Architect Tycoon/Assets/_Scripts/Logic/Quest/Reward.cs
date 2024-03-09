using System;
using UnityEngine;
using Zenject;

public class Reward : ScriptableObject
{
    public string rewardName;
    public Sprite rewardSprite;
    public int rewardAmount;


    public virtual void GiveReward(Progress progress)
    {
    }
}