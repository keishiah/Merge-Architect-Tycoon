using System;
using UniRx;

[Serializable]
public class PlayerRiches
{
    public ReactiveProperty<int> Coins = new();
    public ReactiveProperty<int> Diamonds = new();
}