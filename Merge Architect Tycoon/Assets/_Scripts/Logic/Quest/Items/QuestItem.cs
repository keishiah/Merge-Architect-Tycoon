using System;
using UnityEngine;

[Serializable]
public abstract class QuestItem
{
    public string itemText;
    public string buildingName;
    public Sprite itemImage;
    public int itemCount;

    public abstract int GetCurrentItemCount(Progress progress);
}