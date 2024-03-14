using System;
using UniRx;
using UnityEngine;

[Serializable]
public class Coins : IDisposable
{
    public int CurrentCoinsCount => _coinsCount.Value;

    [SerializeField] private ReactiveProperty<int> _coinsCount = new();
    private ReactiveCommand<int> _onCoinsAdded = new();

    public bool SpendCoins(int count)
    {
        if (_coinsCount.Value < count)
            return false;
        _coinsCount.Value -= count;
        return true;
    }

    public void Add(int count)
    {
        _coinsCount.Value += count;
        _onCoinsAdded.Execute(count);
    }

    public IDisposable SubscribeToCoinsCountChanges(Action<int> onCoinsCountChanged)
    {
        return _coinsCount.Subscribe(onCoinsCountChanged);
    }

    public IDisposable SubscribeToCoinsAdded(Action<int> onCoinsAdded)
    {
        return _onCoinsAdded.Subscribe(onCoinsAdded);
    }

    public void Dispose()
    {
        _coinsCount?.Dispose();
        _onCoinsAdded?.Dispose();
    }
}