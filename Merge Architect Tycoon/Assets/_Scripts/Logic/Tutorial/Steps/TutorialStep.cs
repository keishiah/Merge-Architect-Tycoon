using System;
using UnityEngine;

[Serializable]
public abstract class TutorialStep : ScriptableObject
{
    public abstract void Enter(TutorialHandler tutorialHandler);
}
