public class LoadLevelState : IPaylodedState<string>
{
    private string _sceneName;
    private readonly QuestsPresenter _questsPresenter;

    private readonly SceneContextProvider _sceneContextProvider;

    private IGameStateMachine _gameStateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly PlayerProgress _playerProgressService;

    public LoadLevelState(ISceneLoader sceneLoader, PlayerProgress playerProgressService,
        SceneContextProvider sceneContextProvider)
    {
        _sceneLoader = sceneLoader;
        _playerProgressService = playerProgressService;
        _sceneContextProvider = sceneContextProvider;
    }

    public void Enter(string sceneName)
    {
        _sceneName = sceneName;

        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void SetGameStateMachine(IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Exit()
    {
    }

    private void OnLoaded()
    {
        _sceneContextProvider.SetCurrentSceneContext(_sceneName);
        InitLevel();
    }

    private void InitLevel()
    {
        InitializePopupPresenters();

        _sceneContextProvider.Resolve<QuestGiver>().OnSceneLoaded();
        _sceneContextProvider.Resolve<MergeGrid>().OnSceneLoaded();
    }

    private void InitializePopupPresenters()
    {
        var createBuildingPopupPresenter = _sceneContextProvider.Resolve<CreateBuildingPopupPresenter>();
        createBuildingPopupPresenter.InitializePresenter();
        _sceneContextProvider.Resolve<BuildingProvider>().OnSceneLoaded();
        _sceneContextProvider.Resolve<DistrictsPresenter>().OnSceneLoaded();
        //_sceneContextProvider.Resolve<TruckPresenter>().OnSceneLoaded();
    }
}