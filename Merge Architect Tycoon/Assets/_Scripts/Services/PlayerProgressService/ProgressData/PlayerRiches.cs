using System;
using UniRx;
using UnityEngine;

[Serializable]
public class PlayerRiches : ISerializationCallbackReceiver
{
    [SerializeField] private int coinsValue;

    [SerializeField] private int diamondsValue;

    public ReactiveProperty<int> Coins { get; private set; } = new();
    public ReactiveProperty<int> Diamonds { get; private set; } = new();

    public void OnBeforeSerialize()
    {
        coinsValue = Coins.Value;
        diamondsValue = Diamonds.Value;
    }

    public void OnAfterDeserialize()
    {
        Coins.Value = coinsValue;
        Diamonds.Value = diamondsValue;
    }
}