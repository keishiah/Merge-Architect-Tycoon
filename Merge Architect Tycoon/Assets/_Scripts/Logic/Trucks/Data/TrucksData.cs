using System;
using UniRx;

[Serializable]
public class TrucksData
{
    public ReactiveProperty<int> Level = new();
    public ReactiveProperty<int> BoostCount = new();
    public ReactiveProperty<int> ResourcesCount = new();

    public TruckData[] ToArrive;
}

[Serializable]
public class TruckData
{
    public int Look;
    public string[] Cargo;
}