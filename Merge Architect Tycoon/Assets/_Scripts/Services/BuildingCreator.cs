using System;
using System.Threading;
using _Scripts.Logic.Buildings;
using _Scripts.Services.StaticDataService;
using _Scripts.UI.Presenters;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Scripts.Services
{
    public class BuildingCreator
    {
        private IStaticDataService _staticDataService;

        [Inject]
        void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public async UniTask CreateBuildingInTimeAsync(BuildingPlace buildingPlace, string buildingName,
            CancellationTokenSource cancellationTokenSource)
        {
            var timeToCreate = _staticDataService.GetBuildingData(buildingName).timeToCreate;
            buildingPlace.UpdateTimerText(timeToCreate);

            while (timeToCreate > 0)
            {
                var delayTimeSpan = TimeSpan.FromSeconds(1f);

                await UniTask.Delay(delayTimeSpan, cancellationToken: cancellationTokenSource.Token);
                timeToCreate--;
                buildingPlace.UpdateTimerText(timeToCreate);
            }

            CreateBuilding(buildingPlace);
        }

        private void CreateBuilding(BuildingPlace buildingPlace)
        {
            buildingPlace.SetBuildingState(BuildingStateEnum.BuildingFinished);
        }
    }
}