using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class TruckButtonInit : MonoBehaviour
{
    [Inject] TruckPresenter truckPresenter;
    [SerializeField] GameObject truckMenu;
    [SerializeField] Truck truck;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(AddNewTruck);
    }

    private void AddNewTruck()
    {
        truckPresenter.AddNewTruck(truck);
        truckMenu.SetActive(false);
    }
}
