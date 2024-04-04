using UnityEngine;

public abstract class QuestObjective : ScriptableObject
{
    public string itemText;
    public Sprite itemImage;
    public abstract string GetProgress();
    public abstract bool IsComplete();
}