using System;
using UniRx;
using UnityEngine;

[Serializable]
public class TrucksData
{
    public int UpdateLevel;

    public int ResourceLevel;
    public int CargoCapacity;
    public int LuckLevel;

    public ReactiveProperty<int> BoostCount = new();
    public int BoostBuyLevel;
    public DateTime lastResetDate;

    public int CurrentResource;

    public TruckData[] ToArrive;

    //Statistic
    public Action OnAddTruck;
}

[Serializable]
public class TruckData
{
    public int Look;
    public string[] Cargo;
}