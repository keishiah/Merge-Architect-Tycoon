using UnityEngine;

[CreateAssetMenu(fileName = "LootBox", menuName = "StaticData/Loot/LootBox")]
public class LootBox : ScriptableObject
{
    public Loot[] Loot;

    public T GetRandomItem<T>(int limit = 16) where T : ScriptableObject
    {
        if (limit == 0)
            return (T)Loot[0].Item;

        int allWeight = 0;
        for (int i = 0; i < Loot.Length && i <= limit; i++)
        {
            allWeight += Loot[i].WeightOfChance;
        }

        int cursor = Random.Range(0, allWeight);

        int index = 0;
        while (cursor > Loot[index].WeightOfChance)
        {
            cursor -= Loot[index].WeightOfChance;
            index++;
        }

        return (T)Loot[index].Item;
    }
}