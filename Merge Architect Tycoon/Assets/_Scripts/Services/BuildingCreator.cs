using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

public class BuildingCreator
{
    private IStaticDataService _staticDataService;
    private QuestsProvider _questsProvider;

    [Inject]
    void Construct(IStaticDataService staticDataService, QuestsProvider questsProvider)
    {
        _staticDataService = staticDataService;
        _questsProvider = questsProvider;
    }

    public async UniTask CreateBuildingInTimeAsync(BuildingPlace buildingPlace, string buildingName,
        CancellationTokenSource cancellationTokenSource)
    {
        var timeToCreate = _staticDataService.GetBuildingData(buildingName).timeToCreate;
        buildingPlace.UpdateTimerText(timeToCreate);

        while (timeToCreate > 0)
        {
            var delayTimeSpan = TimeSpan.FromSeconds(1f);

            await UniTask.Delay(delayTimeSpan, cancellationToken: cancellationTokenSource.Token);
            timeToCreate--;
            buildingPlace.UpdateTimerText(timeToCreate);
        }

        CreateBuilding(buildingPlace);
    }

    private void CreateBuilding(BuildingPlace buildingPlace)
    {
        _questsProvider.CheckCompletionTutorialQuest("CreateFirstBuilding");
        _questsProvider.CheckCompletionTutorialQuest("FirstDistrictEarn");
        buildingPlace.SetBuildingState(BuildingStateEnum.BuildingFinished);
    }
}