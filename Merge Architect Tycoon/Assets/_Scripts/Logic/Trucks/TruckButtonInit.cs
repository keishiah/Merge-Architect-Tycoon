using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class TruckButtonInit : MonoBehaviour
{
    [Inject]
    private TruckPresenter _truckPresenter;
    [Inject]
    private IPlayerProgressService _playerProgressService;
    [SerializeField]
    private GameObject _truckMenu;
    [SerializeField]
    private Truck _truck;
    [SerializeField]
    private int _cost;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(AddNewTruck);
    }

    private void AddNewTruck()
    {
        if(_playerProgressService.Progress.Coins.SpendCoins(_cost))
            _truckPresenter.AddNewTruck(_truck.Clone());
        _truckMenu.SetActive(false);
    }
}
