using System.Threading;
using UnityEngine;
using Zenject;

public class BuildingPlace : MonoBehaviour
{
    public BuildingRenderer buildingView;
    public string buildingName;
    [HideInInspector] public int districtId;

    public CancellationTokenSource ActivityToken { get; private set; }

    private StaticDataService _staticDataService;
    private BuildingProvider _buildingProvider;
    private BuildingCreator _buildingCreator;
    private PlayerProgress _playerProgressService;

    [Inject]
    void Construct(StaticDataService staticDataService, BuildingCreator buildingCreator,
        BuildingProvider buildingProvider, PlayerProgress playerProgressService)
    {
        _staticDataService = staticDataService;
        _buildingCreator = buildingCreator;
        _buildingProvider = buildingProvider;
    }

    public void InitializeBuilding(int district)
    {
        districtId = district;
        ActivityToken = new CancellationTokenSource();
        _buildingProvider.AddBuildingPlaceToSceneDictionary(buildingName, this);
    }

    public void SetBuildingState(BuildingStateEnum state)
    {
        switch (state)
        {
            case BuildingStateEnum.Inactive:
                buildingView.SetViewInactive();
                break;
            case BuildingStateEnum.BuildInProgress:
                buildingView.SetViewBuildInProgress();
                buildingView.createBuildingInMomentButton.onClick.AddListener(CreateInMoment);
                break;
            case BuildingStateEnum.CreateBuilding:
                buildingView.SetViewBuildCreated();
                buildingView.ShowBuildSpriteOnCreate(_staticDataService.BuildingInfoDictionary[buildingName].districtSprite);
                break;
            case BuildingStateEnum.ShowBuilding:
                buildingView.SetViewBuildCreated();
                buildingView.ShowBuildingSprite(_staticDataService.BuildingInfoDictionary[buildingName].districtSprite);
                break;
        }
    }

    public void StartCreatingBuilding()
    {
        SetBuildingState(BuildingStateEnum.BuildInProgress);
    }

    public void UpdateTimerText(int totalSeconds)
    {
        buildingView.UpdateTimerText(StaticMethods.FormatTimerText(totalSeconds));
    }


    public void OnApplicationQuit()
    {
        CanselToken();
        ActivityToken?.Dispose();
    }

    public void CanselToken()
    {
        ActivityToken?.Cancel();
    }

    private void CreateInMoment()
    {
        _buildingCreator.CreateInMoment(this,
            _staticDataService.BuildingInfoDictionary[buildingName].diamondsCountToSkip);
    }
}