using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class TruckPresenter : MonoBehaviour
{
    [SerializeField] private Image[] _resources;

    [SerializeField] public float _startXPosition;
    [SerializeField] public float _stopXPosition;
    [SerializeField] public float _endXPosition;
    [SerializeField] public float _speed;

    [Inject] private SlotsManager _slotsManager;
    private Queue<Truck> _trucks = new();
    private TruckBehaviour _truckBehaviour;

    private void Awake()
    {
        _slotsManager.OnNewEmptySlotAppears += () => { enabled = true; };
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
            _resources = _resources,
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

        if (_truckBehaviour.IsComplete)
            NextStage();
    }
    public void UpdateOn()
    {
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
        for (int i = 0; i < _resources.Length; i++)
        {
            if (i < truck.TruckCargo.Count)
            {
                _resources[i].enabled = true;
                _resources[i].sprite = truck.TruckCargo[i].itemSprite;
            }
            else
                _resources[i].enabled = false;
        }
    }
}
