using System;
using UniRx;

[Serializable]
public class PlayerRiches
{
    public ReactiveProperty<int> Coins { get; private set; } = new(500);
    public ReactiveProperty<int> Diamonds { get; private set; } = new(5);
}