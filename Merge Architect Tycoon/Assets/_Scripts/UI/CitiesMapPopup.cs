using _Scripts.UI.Presenters;
using UnityEngine;
using Zenject;

namespace _Scripts.UI
{
    public class CitiesMapPopup : MonoBehaviour
    {
        private DistrictsPresenter _districtsPresenter;

        [Inject]
        void Construct(DistrictsPresenter districtsPresenter)
        {
            _districtsPresenter = districtsPresenter;
            _districtsPresenter.CitiesMapPopup = gameObject;
        }
    }
}