using System;
using System.Threading;
using _Scripts.Services.StaticDataService;
using _Scripts.UI.Elements;
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

        public async UniTask CreateCurrencyInTimeAsync(DistrictUi district)
        {
            float timeToCreate = _staticDataService.GetDistrictData(district.districtId).timeToEarn;
            district.SetSliderValue(timeToCreate);

            while (timeToCreate > 0)
            {
                var delayTimeSpan = TimeSpan.FromSeconds(1f);

                await UniTask.Delay(delayTimeSpan, cancellationToken: district.ActivityToken.Token);
                timeToCreate--;
                district.SetSliderValue(timeToCreate);
            }

        }
    }
}