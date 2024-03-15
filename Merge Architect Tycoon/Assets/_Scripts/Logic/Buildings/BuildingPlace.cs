using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BuildingPlace : MonoBehaviour
{
    public BuildingView buildingView;
    public string buildingName;
    [HideInInspector] public int districtId;

    public CancellationTokenSource ActivityToken { get; private set; }

    private IStaticDataService _staticDataService;
    private BuildingCreator _buildingCreator;
    private BuildingProvider _buildingProvider;

    [Inject]
    void Construct(IStaticDataService staticDataService, BuildingCreator buildingCreator,
        BuildingProvider buildingProvider)
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
                buildingView.ShowBuildInProgressSprite(_staticDataService.BuildInProgressSprite);
                break;
            case BuildingStateEnum.BuildingFinished:
                buildingView.SetViewBuildCreated();
                buildingView.ShowBuildSprite(_staticDataService.GetBuildingData(buildingName)
                    .districtSprite);
                break;
        }
    }

    public UniTask StartCreatingBuilding()
    {
        SetBuildingState(BuildingStateEnum.BuildInProgress);
        return _buildingCreator.CreateBuildingInTimeAsync(this, buildingName, ActivityToken);
    }

    public void UpdateTimerText(int totalSeconds)
    {
        buildingView.UpdateTimerText(StaticMethods.FormatTimerText(totalSeconds));
    }

    public void OnDestroy()
    {
        ActivityToken?.Cancel();
    }
}