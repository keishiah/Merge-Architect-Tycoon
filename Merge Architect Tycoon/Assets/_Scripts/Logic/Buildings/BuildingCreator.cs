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

    public async UniTask CreateBuildingInTimeAsync(BuildingPlace buildingPlace,
        CancellationTokenSource cancellationTokenSource, int creationTime = default)
    {
        string buildingNameTempo = buildingPlace.buildingName;
        int timeToCreate = creationTime == 0
            ? _staticDataService.BuildingInfoDictionary[buildingNameTempo].timeToCreate
            : creationTime;

        _playerProgressService.AddBuildingCreationTimeRest(buildingNameTempo, timeToCreate);

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

    public void CreateInMoment(BuildingPlace buildingPlace, int diamondsCountToSkip)
    {
        if (_playerProgressService.SpendDiamonds(diamondsCountToSkip))
        {
            buildingPlace.CanselToken();
            CreateBuilding(buildingPlace);
        }
    }

    public void CreateBuilding(BuildingPlace buildingPlace)
    {
        _playerProgressService.RemoveBuildingInProgress(buildingPlace.buildingName);
        _playerProgressService.AddBuilding(buildingPlace.buildingName);
        buildingPlace.SetBuildingState(BuildingStateEnum.CreateBuilding);
    }
}