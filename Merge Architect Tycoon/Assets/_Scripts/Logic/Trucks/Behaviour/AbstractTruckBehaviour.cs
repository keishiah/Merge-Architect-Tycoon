using System;

namespace _Scripts.Logic.Trucks.Behaviour
{
    [Serializable]
    public abstract class TruckBehaviour
    {
        public bool IsComplete = false;
        public DateTime _time;

        public virtual void Enter() { }
        public virtual void Update() { }
    }
}
