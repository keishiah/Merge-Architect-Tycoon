using System;
using _Scripts.Logic.Merge;
using _Scripts.Logic.Merge.Items;
using UnityEngine.UI;

namespace _Scripts.Logic.Trucks.Behaviour
{
    public class TruckUnloading : TruckBehaviour
    {
        public SlotsManager _slotsManager;
        public Truck _truck;
        public Image[] _resources;
        public TruckPresenter _truckPresenter;
        public float _unloadSpeed = 1f;

        public override void Update()
        {
            if (_truck.TruckCargo.Count == 0)
            {
                IsComplete = true;
                return;
            }

            TimeSpan inUnloadTime = DateTime.Now - _time;
            float unloadPersentage = (float)inUnloadTime.TotalSeconds / _unloadSpeed;
            if (unloadPersentage < 1)
                return;

            if (_slotsManager.EmptyUnloadSlotsCount == 0)
            {
                _truckPresenter.UpdateOff();
                return;
            }

            MergeItem mergeItem = _truck.DequeueItem();
            foreach (var resource in _resources)
            {
                if (resource.enabled && mergeItem.itemSprite == resource.sprite)
                {
                    resource.enabled = false;
                    break;
                }
            }
            _slotsManager.AddItemToEmptySlot(mergeItem, isToUnloadSlot: true);

            _time = DateTime.Now;
        }
    }
}
