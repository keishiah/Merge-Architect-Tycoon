using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BuildingCreator
{
    private IStaticDataService _staticDataService;
    private IPlayerProgressService _playerProgressService;

    [Inject]
    void Construct(IStaticDataService staticDataService, IPlayerProgressService playerProgressService)
    {
        _staticDataService = staticDataService;
        _playerProgressService = playerProgressService;
    }

    public async UniTask CreateBuildingInTimeAsync(BuildingPlace buildingPlace, string buildingName,
        CancellationTokenSource cancellationTokenSource, int creationTime = default)
    {
        string buildingNameTempo = buildingName;
        int timeToCreate = creationTime == default
            ? _staticDataService.GetBuildingData(buildingName).timeToCreate
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
            }
            catch (OperationCanceledException)
            {
                _playerProgressService.Progress.AddBuildingCreationTimeRest(buildingNameTempo, timeToCreate);
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
        _playerProgressService.Progress.RemoveBuildingInProgress(buildingPlace.buildingName);
    }
}