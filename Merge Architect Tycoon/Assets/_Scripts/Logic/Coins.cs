using System;
using UniRx;
using UnityEngine;

namespace _Scripts.Logic
{
    [Serializable]
    public class Coins
    {
        public int CurrentCoinsCount => _coinsCount.Value;

        [SerializeField] private ReactiveProperty<int> _coinsCount = new();

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
        }

        public IDisposable SubscribeToCoinsCountChanges(Action<int> onCoinsCountChanged)
        {
            return _coinsCount.Subscribe(onCoinsCountChanged);
        }
    }
}