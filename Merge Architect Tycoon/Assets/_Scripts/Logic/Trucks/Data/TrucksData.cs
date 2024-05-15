using System;
using UniRx;

[Serializable]
public class TrucksData
{
    public int UpdateLevel;

    public int ResourceLevel;
    public int CargoCapacity;
    public int LuckLevel;

    public ReactiveProperty<int> BoostCount = new(1);
    public int BoostBuyLevel;
    public DateTime lastResetDate;

    public int CurrentResource;

    public TruckData[] ToArrive;

    //Statistic
    public ReactiveProperty<int> TruckBuyCount = new();
}

[Serializable]
public class TruckData
{
    public int Look;
    public string[] Cargo;
}