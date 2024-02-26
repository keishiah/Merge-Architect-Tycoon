using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    sellable,
    unsellable,
    spawner
}

[CreateAssetMenu(menuName = "Merge Items/Merge Item", order = 2, fileName = "New Merge Item")]
public class MergeItem : ScriptableObject
{
    public string itemName = "";
    [TextArea]
    public string itemDescrpition = "";
    [Range(1, 7)]
    public int itemLevel = 1;
    public float itemCost = 0;

    public ItemType itemType= ItemType.sellable;

    public Sprite itemSprite;

    [Space]
    public MergeItem nextItem;
    [Space]
    public int InItemsCount;
    public MergeItem InItem;
}
