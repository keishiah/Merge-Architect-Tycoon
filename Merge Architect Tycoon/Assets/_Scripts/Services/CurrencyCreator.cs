using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class CurrencyCreator
{
    private StaticDataService _staticDataService;

    [Inject]
    void Construct(StaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
    }

    public async UniTask CreateCurrencyInTimeAsync(DistrictPopup district)
    {
        try
        {
            float timeToCreate = _staticDataService.DistrictsInfoDictionary[district.DistrictId].timeToEarn;
            district.SetSliderMaxValue(timeToCreate);
            district.SetSliderValue(timeToCreate);
            district.CoinsSlider.gameObject.SetActive(true);

            while (timeToCreate > 0)
            {
                var delayTimeSpan = TimeSpan.FromSeconds(0.03f);

                await UniTask.Delay(delayTimeSpan, cancellationToken: district.ActivityToken.Token);
                timeToCreate -= 0.03f;
                district.SetSliderValue(timeToCreate);
            }
        }

        catch (OperationCanceledException)
        {
            return;
        }

        if (!district.ActivityToken.IsCancellationRequested)
        {
            district.TurnOnEarnButton();
        }
    }
}