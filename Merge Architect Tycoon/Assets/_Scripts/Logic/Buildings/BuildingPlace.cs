using System.Threading;
using _Scripts.Logic.CityData;
using _Scripts.Services;
using _Scripts.Services.StaticDataService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Scripts.Logic.Buildings
{
    public enum BuildingStateEnum
    {
        Inactive,
        BuildInProgress,
        BuildingFinished
    }

    public class BuildingPlace : MonoBehaviour
    {
        public BuildingView buildingView;
        public string buildingName;
        [HideInInspector] public int districtId;

        private IStaticDataService _staticDataService;
        private BuildingCreator _buildingCreator;
        private BuildingProvider _buildingProvider;
        private CancellationTokenSource _activityToken;

        [Inject]
        void Construct(IStaticDataService staticDataService, BuildingCreator buildingCreator,
            BuildingProvider buildingProvider)
        {
            _staticDataService = staticDataService;
            _buildingCreator = buildingCreator;
            _buildingProvider = buildingProvider;
        }

        private void Awake()
        {
            districtId = GetComponentInParent<District>().districtId;
            _activityToken = new CancellationTokenSource();
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
                    buildingView.ShowBuildSprite(_staticDataService.BuildInProgressSprite);
                    break;
                case BuildingStateEnum.BuildingFinished:
                    buildingView.SetViewBuildCreated();
                    buildingView.ShowBuildSprite(_staticDataService.GetBuildingData(buildingName)
                        .buildingSprite);
                    break;
            }
        }

        public UniTask StartCreatingBuilding()
        {
            SetBuildingState(BuildingStateEnum.BuildInProgress);

            return _buildingCreator.CreateBuildingInTimeAsync(this, buildingName, _activityToken);
        }

        public void UpdateTimerText(int totalSeconds)
        {
            buildingView.UpdateTimerText(StaticMethods.FormatTimerText(totalSeconds));
        }

        public void OnDestroy()
        {
            _activityToken?.Cancel();
            _activityToken?.Dispose();
            _activityToken = null;
        }
    }
}