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
    void Construct(IStaticDataService staticDataService, IPlayerProgressService playerProgressService,
        EffectsProvider effectsProvider)
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
                _playerProgressService.Progress.AddBuildingCreationTimeRest(buildingNameTempo, timeToCreate);
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
        _playerProgressService.Progress.RemoveBuildingInProgress(buildingPlace.buildingName);
        // buildingPlace.SetBuildingState(BuildingStateEnum.Inactive);
        buildingPlace.SetBuildingState(BuildingStateEnum.CreateBuilding);
    }
}