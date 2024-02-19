using System;
using System.Threading;
using CodeBase.Logic.Buildings;
using CodeBase.Services.StaticDataService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Services
{
    public class BuildingCreator
    {
        private IStaticDataService _staticDataService;

        [Inject]
        void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public async UniTaskVoid CreateBuildingInTimeAsync(BuildingPlace buildingPlace, string buildingName,
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