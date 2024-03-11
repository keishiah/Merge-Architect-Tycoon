using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

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

        try
        {
            while (timeToCreate > 0)
            {
                var delayTimeSpan = TimeSpan.FromSeconds(1f);

                await UniTask.Delay(delayTimeSpan, cancellationToken: cancellationTokenSource.Token);
                timeToCreate--;
                buildingPlace.UpdateTimerText(timeToCreate);
            }

            cancellationTokenSource.Token.ThrowIfCancellationRequested();

            CreateBuilding(buildingPlace);
        }
        catch (OperationCanceledException)
        {
            buildingPlace.SetBuildingState(BuildingStateEnum.Inactive);
        }
    }

    private void CreateBuilding(BuildingPlace buildingPlace)
    {
        buildingPlace.SetBuildingState(BuildingStateEnum.BuildingFinished);
    }
}