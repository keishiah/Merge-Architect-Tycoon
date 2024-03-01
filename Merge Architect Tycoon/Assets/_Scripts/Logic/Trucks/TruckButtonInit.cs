using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Logic.Trucks
{
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
            truckPresenter.AddNewTruck(truck.Clone());
            truckMenu.SetActive(false);
        }
    }
}
