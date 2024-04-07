using UnityEngine;

public abstract class QuestObjective : ScriptableObject
{
    public Sprite Sprite;

    public abstract string GetDescription();
    public abstract string GetProgressText(QuestProgress questProgress);
    public abstract void DoSubscribe(PlayerProgress playerProgress, QuestProgress questProgress);
    public abstract bool IsComplete(PlayerProgress playerProgress, QuestProgress questProgress = null);
}