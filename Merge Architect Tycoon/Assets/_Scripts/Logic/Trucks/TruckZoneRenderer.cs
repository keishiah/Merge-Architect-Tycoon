using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class TruckZoneRenderer : MonoBehaviour
{
    [SerializeField] public float _startXPosition;
    [SerializeField] public float _stopXPosition;
    [SerializeField] public float _endXPosition;

    [Inject] private SlotsManager _slotsManager;
    [Inject] private PlayerProgress _playerProgress;
    [Inject] private TruckProvider _truckProvider;

    [SerializeField] private GameObject _resourcePrefab;
    [SerializeField] private Transform _resourceHolder;

    private TruckBehaviour _truckBehaviour;
    private Truck _currentTruck;
    private float _lastTruckSpeed;
    public bool _isNeedToUnload { get; private set; }

    private void Awake()
    {
        _slotsManager.OnNewEmptySlotAppears += UpdateOn;
    }

    private void OnEnable()
    {
        if (_playerProgress.Trucks == null || _playerProgress.Trucks.ToArrive == null)
            return;

        if (_playerProgress.Trucks.ToArrive.Length > 0 && _currentTruck == null)
            ToNextTruck();
    }

    private void NextStage()
    {
        switch (_truckBehaviour)
        {
            case TruckToUnload:
                NextStageUnloadind();
                break;
            case TruckUnloading:
                NextStageGoAway();
                break;
            case TruckGoAway:
                ToNextTruck();
                if (_truckBehaviour == null)
                    return;
                break;
            default:
                throw new NotImplementedException("Unknown type of truck behavior!");
        }
    }
    private void ToNextTruck()
    {
        if (_playerProgress.Trucks.ToArrive.Length == 0)
        {
            UpdateOff();
            _truckBehaviour = null;
            _currentTruck = null;
            return;
        }

        if(_currentTruck == null)
            _currentTruck = _truckProvider.Pop();

        PaintTheTruck();

        _truckBehaviour = new TruckToUnload()
        {
            RectTtransform = GetComponent<RectTransform>(),
            StartXPosition = _startXPosition,
            EndXPosition = _stopXPosition,
            Speed = _currentTruck.Speed,
        };
        _truckBehaviour.Enter();
    }
    private void NextStageUnloadind()
    {
        _truckBehaviour = new TruckUnloading()
        {
            _slotsManager = _slotsManager,
            _truck = _currentTruck,
            _truckZone = this,
        };
        _truckBehaviour.Enter();
    }

    private void NextStageGoAway()
    {
        _lastTruckSpeed = _currentTruck.Speed;
        _currentTruck = _truckProvider.Dequeue();

        _truckBehaviour = new TruckGoAway()
        {
            RectTtransform = GetComponent<RectTransform>(),
            StartXPosition = _stopXPosition,
            EndXPosition = _endXPosition,
            Speed = _currentTruck != null ? _currentTruck.Speed : _lastTruckSpeed,
        };
        _truckBehaviour.Enter();
    }

    private void Update()
    {
        if (_truckBehaviour == null)
        {
            UpdateOff();
            return;
        }

        _truckBehaviour.Update();
        _isNeedToUnload = false;

        if (_truckBehaviour.IsComplete)
            NextStage();
    }

    public void ChanceTruckSpeed(int newSpeed)
    {
        if(_currentTruck != null)
            _currentTruck.Speed = newSpeed;
        if(_truckBehaviour != null)
            switch (_truckBehaviour)
            {
                case TruckToUnload truckToUnload:
                    truckToUnload.Speed = newSpeed;
                    break;
                case TruckGoAway truckGoAway:
                    truckGoAway.Speed = newSpeed;
                    break;
            }
    }
    public void ReadyToUnload() => _isNeedToUnload = true;
    public void UpdateOn()
    {
        _isNeedToUnload = false;
        enabled = true;
    }
    public void UpdateOff()
    {
        enabled = false;
    }
    private void PaintTheTruck()
    {
        //GetComponent<Image>().sprite = truck.SpriteImage;
        List<MergeItem> mergeItems = _currentTruck.TruckCargo;
        int spritesCount = _resourceHolder.childCount;
        for (int i = 0; i < mergeItems.Count; i++)
        {
            if (i < spritesCount)
            {
                Transform resourceTransform = _resourceHolder.GetChild(i);
                resourceTransform.gameObject.SetActive(true);
                resourceTransform.GetComponentInChildren<Image>().sprite = mergeItems[i].ItemSprite;
            }
            else
            {
                GameObject newResource = Instantiate(_resourcePrefab, _resourceHolder);
                newResource.GetComponentInChildren<Image>().sprite = mergeItems[i].ItemSprite;
            }
        }
    }

    public void DequeueItem(int index)
    {
        _resourceHolder.GetChild(index).gameObject.SetActive(false);
    }
}