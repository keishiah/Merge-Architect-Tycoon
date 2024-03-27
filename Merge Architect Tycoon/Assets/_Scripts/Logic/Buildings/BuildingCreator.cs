using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

public class BuildingCreator
{
    private StaticDataService _staticDataService;
    private PlayerProgressService _playerProgressService;

    [Inject]
    void Construct(StaticDataService staticDataService, PlayerProgressService playerProgressService)
    {
        _staticDataService = staticDataService;
        _playerProgressService = playerProgressService;
    }

    public async UniTask CreateBuildingInTimeAsync(BuildingPlace buildingPlace, string buildingName,
        CancellationTokenSource cancellationTokenSource, int creationTime = default)
    {
        string buildingNameTempo = buildingName;
        int timeToCreate = creationTime == default
            ? _staticDataService.BuildingInfoDictionary[buildingName].timeToCreate
            : creationTime;
        buildingPlace.UpdateTimerText(timeToCreate);

        while (timeToCreate > 0 && !cancellationTokenSource.IsCancellationRequested)
        {
            var delayTimeSpan = TimeSpan.FromSeconds(1f);
            try
            {
                await UniTask.Delay(delayTimeSpan, cancellationToken: cancellationTokenSource.Token);
                timeToCreate--;
                buildingPlace.UpdateTimerText(timeToCreate);
                _playerProgressService.AddBuildingCreationTimeRest(buildingNameTempo, timeToCreate);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }

        if (!cancellationTokenSource.IsCancellationRequested)
        {
            CreateBuilding(buildingPlace);
        }
    }


    private void CreateBuilding(BuildingPlace buildingPlace)
    {
        buildingPlace.SetBuildingState(BuildingStateEnum.BuildingFinished);
        _playerProgressService.RemoveBuildingInProgress(buildingPlace.buildingName);
    }
}