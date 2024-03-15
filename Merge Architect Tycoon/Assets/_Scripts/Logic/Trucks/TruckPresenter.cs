using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class TruckPresenter : MonoBehaviour
{

    [SerializeField] public float _startXPosition;
    [SerializeField] public float _stopXPosition;
    [SerializeField] public float _endXPosition;
    [SerializeField] public float _speed;

    [Inject] private SlotsManager _slotsManager;
    [SerializeField] private GameObject _resourcePrefab;
    [SerializeField] private Transform _resourceHolder;
    private Queue<Truck> _trucks = new();
    private TruckBehaviour _truckBehaviour;
    public bool _isNeedToUnload { get; private set; }

    private void Awake()
    {
        _slotsManager.OnNewEmptySlotAppears += UpdateOn;
    }

    private void OnEnable()
    {
        if (_trucks.Count > 0 && _truckBehaviour == null)
            ToNextTruck();
    }

    private void NextStage()
    {
        switch (_truckBehaviour)
        {
            case TruckToUnload behaviour:
                NextStageUnloadind();
                break;
            case TruckUnloading behaviour:
                NextStageGoAway();
                break;
            case TruckGoAway behaviour:
                _trucks.Dequeue();
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
        if (_trucks.Count == 0)
        {
            UpdateOff();
            _truckBehaviour = null;
            return;
        }

        PaintTheTruck();

        _truckBehaviour = new TruckToUnload()
        {
            _rectTtransform = GetComponent<RectTransform>(),
            _startXPosition = _startXPosition,
            _endXPosition = _stopXPosition,
            _speed = _speed,
        };
        _truckBehaviour.Enter();
    }
    private void NextStageUnloadind()
    {
        _truckBehaviour = new TruckUnloading()
        {
            _slotsManager = _slotsManager,
            _truck = _trucks.Peek(),
            _truckPresenter = this,
        };
        _truckBehaviour.Enter();
    }
    private void NextStageGoAway()
    {
        _truckBehaviour = new TruckGoAway()
        {
            _rectTtransform = GetComponent<RectTransform>(),
            _startXPosition = _stopXPosition,
            _endXPosition = _endXPosition,
            _speed = _speed,
        };
        _truckBehaviour.Enter();
    }

    public void AddNewTruck(Truck truck)
    {
        _trucks.Enqueue(truck);
        UpdateOn();
    }
    private void Update()
    {
        if (_truckBehaviour == null)
        {
            UpdateOff();
            return;
        }

        _truckBehaviour.Update();
        Refresh();

        if (_truckBehaviour.IsComplete)
            NextStage();
    }

    private void Refresh()
    {
        _isNeedToUnload = false;
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
        Truck truck = _trucks.Peek();
        GetComponent<Image>().sprite = truck.SpriteImage;
        List<MergeItem> mergeItems = truck.TruckCargo;
        int spritesCount = _resourceHolder.childCount;
        for (int i = 0; i < mergeItems.Count; i++)
        {
            if(i < spritesCount)
            {
                Transform resourceTransform = _resourceHolder.GetChild(i);
                resourceTransform.gameObject.SetActive(true);
                Image resourceSprite = resourceTransform.GetComponentInChildren<Image>();
                resourceSprite.sprite = truck.TruckCargo[i].itemSprite;
            }
            else
            {
                GameObject newResource = Instantiate(_resourcePrefab, _resourceHolder);
                newResource.GetComponentInChildren<Image>().sprite = truck.TruckCargo[i].itemSprite;
            }
        }
    }

    public void DequeueItem(int index)
    {
        _resourceHolder.GetChild(index).gameObject.SetActive(false);
    }
}