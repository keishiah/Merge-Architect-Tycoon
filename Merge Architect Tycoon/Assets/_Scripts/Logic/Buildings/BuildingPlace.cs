using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildingPlace : MonoBehaviour
{
    public BuildingRenderer buildingView;
    public string buildingName;
    [HideInInspector] public int districtId;

    public CancellationTokenSource ActivityToken { get; private set; }

    protected BuildingCreator _buildingCreator;
    protected StaticDataService _staticDataService;
    private BuildingProvider _buildingProvider;
    private PlayerProgress _playerProgressService;
    private CameraZoomer _cameraZoomer;

    [Inject]
    void Construct(StaticDataService staticDataService, BuildingCreator buildingCreator,
        BuildingProvider buildingProvider, PlayerProgress playerProgressService, CameraZoomer cameraZoomer)
    {
        _staticDataService = staticDataService;
        _buildingCreator = buildingCreator;
        _buildingProvider = buildingProvider;
        _cameraZoomer = cameraZoomer;
    }

    public virtual void InitializeBuilding(int district)
    {
        districtId = district;
        ActivityToken = new CancellationTokenSource();
        _buildingProvider.AddBuildingPlaceToSceneDictionary(buildingName, this);
        buildingView.SetBuildingName(buildingName);
        buildingView.buildingButton.OnClickAsObservable().Subscribe(
            _ => _cameraZoomer.ZoomButtonClicked(buildingView.buildingButton.transform, buildingName));
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
                buildingView.SetSkipButtonsText(_staticDataService.BuildingInfoDictionary[buildingName]
                    .diamondsCountToSkip.ToString());
                break;
            case BuildingStateEnum.CreateBuilding:
                buildingView.SetViewBuildCreated();
                ShowBuildingSprite();
                break;
            case BuildingStateEnum.ShowBuilding:
                buildingView.SetViewBuildCreated();
                SHowBuilding();
                break;
        }
    }

    public virtual void SHowBuilding()
    {
        buildingView.ShowBuildingSprite(_staticDataService.BuildingInfoDictionary[buildingName].districtSprite);
    }

    public virtual void ShowBuildingSprite()
    {
        buildingView.ShowBuildSpriteOnCreate(_staticDataService.BuildingInfoDictionary[buildingName]
            .districtSprite);
    }

    public void StartCreatingBuilding() => SetBuildingState(BuildingStateEnum.BuildInProgress);

    public void UpdateTimerText(int totalSeconds) =>
        buildingView.UpdateTimerText(StaticMethods.FormatTimerText(totalSeconds));


    public void OnApplicationQuit()
    {
        CanselToken();
        ActivityToken?.Dispose();
    }

    public void CanselToken()
    {
        ActivityToken?.Cancel();
    }

    public virtual void CreateInMoment()
    {
        _buildingCreator.CreateInMoment(this,
            _staticDataService.BuildingInfoDictionary[buildingName].diamondsCountToSkip);
    }

    public Transform GetBuildingButtonTransform() => buildingView.buildingButton.transform;
}