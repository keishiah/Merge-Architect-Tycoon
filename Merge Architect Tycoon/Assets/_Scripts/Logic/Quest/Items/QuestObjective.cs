using System;
using UnityEngine;

[Serializable]
public abstract class QuestObjective
{
    public string itemText;
    public Sprite itemImage;
    public int GoalCount;

    public virtual int GetCurrentItemCount() => GoalCount;
}