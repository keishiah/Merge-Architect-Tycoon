using UnityEngine;

public abstract class Reward : ScriptableObject
{
    public bool IsHiden;

    public Sprite Sprite;
    public int Amount;

    public abstract void GiveReward(PlayerProgressService progress);
}
