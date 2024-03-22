using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GirlsTruckPurchase : MonoBehaviour
{
    [Inject]
    private TruckPresenter _truckPresenter;
    [Inject]
    private IPlayerProgressService _playerProgressService;
    [SerializeField] private Truck _truck;
    [SerializeField] private List<MergeItem> _randomItemsPool;

    [SerializeField] private Button _buyButton;

    void Start()
    {
        if (_truck == null)
            throw new Exception("The truck data is not filled in!");
        if (_truck.SpriteImage == null)
            throw new Exception("The truck sprite is not filled in!");

        _buyButton.onClick.AddListener(AddNewTruck);
    }

    private void AddNewTruck()
    {
        Truck truck = new Truck();
        truck.SpriteImage = _truck.SpriteImage;
        var TruckCargo = new List<MergeItem>();
        for(int i = 0; i < 3; i++)
        {
            int randInt = UnityEngine.Random.Range(0, 4);
            TruckCargo.Add(_randomItemsPool[randInt]);
        }
        truck.TruckCargo = TruckCargo;

        _truckPresenter.AddNewTruck(truck);
        _playerProgressService.Quests.AddTruckItem();
    }
}