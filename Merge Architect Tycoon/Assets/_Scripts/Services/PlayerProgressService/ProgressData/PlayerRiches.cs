using System;
using UniRx;

[Serializable]
public class PlayerRiches
{
    public ReactiveProperty<int> Coins = new(500);
    public ReactiveProperty<int> Diamonds = new(5);
}