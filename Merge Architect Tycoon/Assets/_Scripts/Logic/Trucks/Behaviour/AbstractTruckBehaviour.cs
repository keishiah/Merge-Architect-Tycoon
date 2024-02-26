using System;
using System.Collections.Generic;

[Serializable]
public abstract class TruckBehaviour
{
    public bool IsComplete = false;
    public DateTime _time;

    public virtual void Enter() { }
    public virtual void Update() { }
}
