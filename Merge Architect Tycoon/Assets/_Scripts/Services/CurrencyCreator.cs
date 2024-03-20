using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

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
        try
        {
            float timeToCreate = _staticDataService.GetDistrictData(district.districtId).timeToEarn;
            district.SetSliderMaxValue(timeToCreate);
            district.coinsSlider.gameObject.SetActive(true);

            while (timeToCreate > 0)
            {
                var delayTimeSpan = TimeSpan.FromSeconds(1f);

                await UniTask.Delay(delayTimeSpan, cancellationToken: district.ActivityToken.Token);
                timeToCreate--;
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