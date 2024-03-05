using Zenject;

public class QuestsPresenter
{
    private IPlayerProgressService _playerProgressService;

    private QuestPopup _questPopup;
    private CreateBuildingPopupPresenter _createBuildingPopupPresenter;
    private LoadLevelState _loadLevelState;

    [Inject]
    void Construct(IPlayerProgressService playerProgressService,
        CreateBuildingPopupPresenter createBuildingPopupPresenter, LoadLevelState loadLevelState)
    {
        _playerProgressService = playerProgressService;
        _createBuildingPopupPresenter = createBuildingPopupPresenter;
        _loadLevelState = loadLevelState;
    }

    public void InitializeWidget()
    {
        _playerProgressService.Progress.Coins.SubscribeToCoinsCountChanges(SetWidgetValues);
    }


    public void SetQuestWidget(QuestPopup questPopup)
    {
        _questPopup = questPopup;
    }

    private void SetWidgetValues(int coins)
    {
        // _createBuildingPopupPresenter.InitializeBuildingInfo();
        // _createBuildingPopupPresenter.SortBuildingElements();
        // var buildingInfo = _createBuildingPopupPresenter._buildings[0];
        // _questWidget.RenderElement1(buildingInfo.buildingName, buildingInfo.coinsCountToCreate, 1,
        //     buildingInfo.itemToCreate);
    }
}