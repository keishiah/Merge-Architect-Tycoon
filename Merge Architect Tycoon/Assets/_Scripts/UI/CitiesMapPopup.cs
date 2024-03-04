using UnityEngine;
using Zenject;

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