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
        PlaceToBuild,
        BuildInProgress,
        BuildingFinished
    }

    public class BuildingPlace : MonoBehaviour
    {
        public BuildingView buildingView;

        private string _buildingToCreateName;

        private IStaticDataService _staticDataService;
        private UiPresenter _uiPresenter;
        private BuildingCreator _buildingCreator;

        private CancellationTokenSource _activityToken;


        [Inject]
        void Construct(IStaticDataService staticDataService, UiPresenter uiPresenter, BuildingCreator buildingCreator)
        {
            _staticDataService = staticDataService;
            _uiPresenter = uiPresenter;
            _buildingCreator = buildingCreator;
            _activityToken = new CancellationTokenSource();
        }

        public void SetBuildingState(BuildingStateEnum state)
        {
            switch (state)
            {
                case BuildingStateEnum.Inactive:
                    buildingView.SetViewInactive();
                    break;
                case BuildingStateEnum.PlaceToBuild:
                    buildingView.SetViewPlaceToBuild();
                    buildingView.ShowBuildSprite(_staticDataService.PlaceToBuildSprite);
                    SubscribeToOpenCreateBuildingPopup();
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

        public void StartCreatingBuilding(string buildingToCreateName)
        {
            _buildingToCreateName = buildingToCreateName;
            SetBuildingState(BuildingStateEnum.BuildInProgress);
            _buildingCreator.CreateBuildingInTimeAsync(this, buildingToCreateName, _activityToken).Forget();
        }

        private void SubscribeToOpenCreateBuildingPopup()
        {

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