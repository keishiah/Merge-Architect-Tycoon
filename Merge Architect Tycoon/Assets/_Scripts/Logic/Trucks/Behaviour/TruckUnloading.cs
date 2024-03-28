using UnityEngine.UI;

public class TruckUnloading : TruckBehaviour
{
    public SlotsManager _slotsManager;
    public Truck _truck;
    public Image[] _resources;
    public TruckZoneRenderer _truckZone;

    public override void Update()
    {
        if (_truck.TruckCargo.Count == 0)
        {
            IsComplete = true;
            return;
        }

        if (!_truckZone._isNeedToUnload)
            return;

        if (_slotsManager.EmptyUnloadSlotsCount == 0)
        {
            _truckZone.UpdateOff();
            return;
        }

        MergeItem mergeItem = _truck.DequeueItem();
        _truckZone.DequeueItem(_truck.TruckCargo.Count);
        _slotsManager.AddItemToEmptySlot(mergeItem, isToUnloadSlot: true);
    }
}