using System;
using UnityEngine;

[Serializable]
public abstract class TutorialStep : ScriptableObject
{
    public bool IsKeyStep;

    public abstract void Enter(TutorialHandler tutorialHandler);
}
