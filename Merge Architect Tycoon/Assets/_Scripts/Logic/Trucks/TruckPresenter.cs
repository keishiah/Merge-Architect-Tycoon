using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class TruckPresenter : MonoBehaviour
{
    [SerializeField] private float _startXPosition;
    [SerializeField] private float _stopXPosition;
    [SerializeField] private float _endXPosition;

    [SerializeField] private float _speed = 5f;

    [SerializeField] private Image[] _resources;

    [Inject] private SlotsManager slotsManager;

    private RectTransform _rectTtransform;
    private Queue<Truck> _trucks = new();
    private DateTime _time;
    private bool isTruckFull => _trucks.Count > 0 && _trucks.Peek().TruckCargo.Count > 0;
    private bool isUnloading;

    private void Awake()
    {
        slotsManager.OnNewEmptySlotAppears += () => { enabled = true; };
        _rectTtransform = GetComponent<RectTransform>();
        _rectTtransform.anchoredPosition = new Vector2(_startXPosition, 0);
    }
    public void AddNewTruck(Truck truck)
    {
        bool needToPaint = false;

        if(_trucks.Count == 0)
        {
            _time = DateTime.Now;
            needToPaint = true;
        }

        _trucks.Enqueue(truck);

        if(needToPaint)
            PaintTheTruck();

        enabled = true;
    }

    private void Update()
    {
        if (_trucks.Count == 0)
        {
            enabled = false;
            return;
        }

        if(_rectTtransform.anchoredPosition.x > _endXPosition)
        {
            ToNewTruck();
        }

        if(isTruckFull && _rectTtransform.anchoredPosition.x >= _stopXPosition)
        {
            if(!isUnloading)
                Unloading();
            return;
        }

        //move truck
        float startPosition = isTruckFull ? _startXPosition : _stopXPosition;
        TimeSpan inWayTime = DateTime.Now - _time;
        float traversedPath = (float)inWayTime.TotalSeconds * _speed;
        _rectTtransform.anchoredPosition = new Vector2(startPosition + traversedPath, 0);
    }

    private async void Unloading()
    {
        isUnloading = true;

        while (true)
        {
            if (_trucks.Peek().TruckCargo.Count == 0)
            {
                isUnloading = false;
                _time = DateTime.Now;
                return;
            }

            await Task.Delay(1000);

            if (slotsManager.EmptySlotsCount == 0)
            {
                enabled = false;
                return;
            }

            MergeItem mergeItem = _trucks.Peek().DequeueItem();
            foreach(var resource in _resources)
            {
                if(resource.enabled && mergeItem.itemSprite == resource.sprite)
                {
                    resource.enabled = false;
                    break;
                }
            }
            slotsManager.AddItemToEmptySlot(mergeItem, isToUnloadSlot: true);
        }
    }

    private void ToNewTruck()
    {
        _trucks.Dequeue();
        _rectTtransform.anchoredPosition = new Vector2(_startXPosition, 0);

        if(_trucks.Count == 0)
        {
            enabled = false;
            return;
        }

        PaintTheTruck();
        _time = DateTime.Now;
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
