using System;
using UniRx;

[Serializable]
public class TruckData
{
    public ReactiveProperty<int> Level = new();
    public ReactiveProperty<int> BoostCount = new();
    public ReactiveProperty<int> ResourcesCount = new();
}
