﻿using System;
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
        EffectsPresenter effectsPresenter)
    {
        _staticDataService = staticDataService;
        _playerProgressService = playerProgressService;
    }

    public async UniTask CreateBuildingInTimeAsync(BuildingPlace buildingPlace,
        CancellationTokenSource cancellationTokenSource, int creationTime = default)
    {
        string buildingNameTempo = buildingPlace.buildingName;
        int timeToCreate = creationTime == default
            ? _staticDataService.GetBuildingData(buildingPlace.buildingName).timeToCreate
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

    public void CreateInMoment(BuildingPlace buildingPlace, int diamondsCountToSkip)
    {
        if (!_playerProgressService.Progress.Diamonds.SpendCoins(diamondsCountToSkip))
            return;
        buildingPlace.CanselToken();
        CreateBuilding(buildingPlace);
    }

    private void CreateBuilding(BuildingPlace buildingPlace)
    {
        _playerProgressService.Progress.RemoveBuildingInProgress(buildingPlace.buildingName);
        _playerProgressService.Progress.AddBuilding(buildingPlace.buildingName);
        buildingPlace.SetBuildingState(BuildingStateEnum.CreateBuilding);
    }
}