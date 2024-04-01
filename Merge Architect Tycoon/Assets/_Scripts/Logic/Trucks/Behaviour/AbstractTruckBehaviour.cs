using System;

[Serializable]
public abstract class TruckBehaviour
{
    public bool IsComplete = false;
    public DateTime Time;

    public virtual void Enter() { }
    public virtual void Update() { }
}
