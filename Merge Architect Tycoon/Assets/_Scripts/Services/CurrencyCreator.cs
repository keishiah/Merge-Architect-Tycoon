using Cysharp.Threading.Tasks;
using System;
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
            float timeToCreate = _staticDataService.DistrictsInfoDictionary[district.districtId].timeToEarn;
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