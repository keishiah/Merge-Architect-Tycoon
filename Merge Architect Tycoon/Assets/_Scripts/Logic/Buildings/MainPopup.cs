using UnityEngine;
using Zenject;

public class MainPopup : MonoBehaviour
{
    public MainPlace MainPlace;
    [Inject] private BuildingProvider _buildingProvider;

    private void OnEnable()
    {
        _buildingProvider.GetNextMainPart(out var mainInfo);
        if (mainInfo != default)
        {
            MainPlace.SetBuildingState(BuildingStateEnum.Inactive);
            MainPlace.buildingName = mainInfo.buildingName;
            _buildingProvider.CreateMainInTimeAsync(MainPlace);
        }
    }
}