using System;
using System.Threading;
using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.StaticDataService;
using CodeBase.UI;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Logic.Buildings
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

        private string _buildingToCreateName;

        private IStaticDataService _staticDataService;
        private BuildingCreator _buildingCreator;
        private CancellationTokenSource _activityToken;
        private BuildingProvider _buildingProvider;


        [Inject]
        void Construct(IStaticDataService staticDataService, BuildingCreator buildingCreator,
            BuildingProvider buildingProvider)
        {
            _staticDataService = staticDataService;
            _buildingCreator = buildingCreator;
            _buildingProvider = buildingProvider;

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
                    buildingView.ShowBuildSprite(_staticDataService.GetBuildingData(_buildingToCreateName)
                        .buildingSprite);
                    break;
            }
        }

        public void StartCreatingBuilding()
        {
            SetBuildingState(BuildingStateEnum.BuildInProgress);

            _buildingCreator.CreateBuildingInTimeAsync(this, buildingName, _activityToken).Forget();
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