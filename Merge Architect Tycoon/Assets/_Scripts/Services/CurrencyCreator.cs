using System;
using System.Threading;
using _Scripts.Services.StaticDataService;
using _Scripts.UI.Elements;
using Cysharp.Threading.Tasks;
using UnityEngine;
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
            district.coinsSlider.gameObject.SetActive(true);
            float timeToCreate = _staticDataService.GetDistrictData(district.districtId).timeToEarn;
            district.SetSliderMaxValue(timeToCreate);

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