using System;
using System.Threading;
using _Scripts.UI.Elements;
using CodeBase.Logic.Buildings;
using CodeBase.Services;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Scripts.Services
{
    public class CurrencyCreator
    {
        private IStaticDataService _staticDataService;

        [Inject]
        void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        public async UniTask CreateCurrencyInTimeAsync(DistrictUi district, string districtName,
            CancellationTokenSource cancellationTokenSource)
        {
            // var timeToCreate = _staticDataService.GetBuildingData(buildingName).timeToCreate;
            // buildingPlace.UpdateTimerText(timeToCreate);
            //
            // while (timeToCreate > 0)
            // {
            //     var delayTimeSpan = TimeSpan.FromSeconds(1f);
            //
            //     await UniTask.Delay(delayTimeSpan, cancellationToken: cancellationTokenSource.Token);
            //     timeToCreate--;
            //     buildingPlace.UpdateTimerText(timeToCreate);
            // }
            //
            // CreateBuilding(buildingPlace);
        }
    }
}