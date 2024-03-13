using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MergeTierQuests : ISerializationCallbackReceiver
{
    [SerializeField] private List<int> itemTier = new();
    [SerializeField] private List<int> itemCount = new();

    private Dictionary<int, int> _itemByTierMergeCount = new();

    public int GetCurrentItemCount(int tier)
    {
        return _itemByTierMergeCount.ContainsKey(tier) ? _itemByTierMergeCount[tier] : 0;
    }

    public void AddTierMergeCount(int tier)
    {
        if (_itemByTierMergeCount.ContainsKey(tier))
        {
            _itemByTierMergeCount[tier] += 1;
        }
        else
        {
            _itemByTierMergeCount.Add(tier, 1);
        }
    }

    public void OnBeforeSerialize()
    {
        itemTier.Clear();
        itemCount.Clear();
        foreach (var kvp in _itemByTierMergeCount)
        {
            itemTier.Add(kvp.Key);
            itemCount.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        _itemByTierMergeCount.Clear();

        for (int i = 0; i < itemTier.Count; i++)
        {
            _itemByTierMergeCount[itemTier[i]] = itemCount[i];
        }
    }
}