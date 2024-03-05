using UnityEngine.UI;

public class TruckUnloading : TruckBehaviour
{
    public SlotsManager _slotsManager;
    public Truck _truck;
    public Image[] _resources;
    public TruckPresenter _truckPresenter;

    public override void Update()
    {
        if (_truck.TruckCargo.Count == 0)
        {
            IsComplete = true;
            return;
        }

        if (!_truckPresenter._isNeedToUnload)
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
    }
}