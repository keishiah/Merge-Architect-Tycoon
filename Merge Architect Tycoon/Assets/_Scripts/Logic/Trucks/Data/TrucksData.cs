using System;

[Serializable]
public class TrucksData
{
    public int UpdateLevel;
    public int BoostCount;
    public int ResourceLevel;
    public int CargoCapacity;
    public int LuckLevel;

    public int CurrentResource;

    public TruckData[] ToArrive;
}

[Serializable]
public class TruckData
{
    public int Look;
    public string[] Cargo;
}