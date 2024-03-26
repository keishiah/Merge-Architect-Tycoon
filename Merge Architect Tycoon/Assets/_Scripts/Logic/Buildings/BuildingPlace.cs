using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class BuildingPlace : MonoBehaviour
{
    public BuildingView buildingView;
    public string buildingName;
    public Transform buildingImagePosition;
    [HideInInspector] public int districtId;

    public CancellationTokenSource ActivityToken { get; private set; }

    private IStaticDataService _staticDataService;
    private BuildingProvider _buildingProvider;
    private IPlayerProgressService _playerProgressService;

    [Inject]
    void Construct(IStaticDataService staticDataService, BuildingCreator buildingCreator,
        BuildingProvider buildingProvider, IPlayerProgressService playerProgressService)
    {
        _staticDataService = staticDataService;
        _buildingProvider = buildingProvider;
        _playerProgressService = playerProgressService;
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
                break;
            case BuildingStateEnum.CreateBuilding:
                buildingView.SetViewBuildCreated();
                buildingView.ShowBuildSpriteOnCreate(_staticDataService.GetBuildingData(buildingName)
                    .districtSprite);
                break;
            case BuildingStateEnum.ShowBuilding:
                buildingView.SetViewBuildCreated();
                buildingView.ShowBuildingSprite(_staticDataService.GetBuildingData(buildingName)
                    .districtSprite);
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
        ActivityToken?.Cancel();
        ActivityToken?.Dispose();
    }
}