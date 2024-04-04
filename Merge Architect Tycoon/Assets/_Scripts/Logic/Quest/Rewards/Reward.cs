using UnityEngine;

public abstract class Reward : ScriptableObject
{
    public Sprite rewardSprite;
    public int rewardAmount;

    public abstract void GiveReward(PlayerProgressService progress);
}