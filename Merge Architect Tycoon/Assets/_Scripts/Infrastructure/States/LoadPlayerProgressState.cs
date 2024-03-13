public class LoadPlayerProgressState : IState
{
    private IGameStateMachine _gameStateMachine;

    private readonly IPlayerProgressService _progressService;

    public LoadPlayerProgressState(IPlayerProgressService progressService)
    {
        _progressService = progressService;
    }

    public void SetGameStateMachine(IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();

        _gameStateMachine.Enter<LoadLevelState, string>(AssetPath.StartGameScene);
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew()
    {
        _progressService.Progress =
            SaveLoadService.Load<Progress>(SaveKey.Progress);
        if (_progressService.Progress == null)
            _progressService.Progress = new Progress();

        _progressService.Quests = SaveLoadService.Load<Quests>(SaveKey.Quests);
        if (_progressService.Quests == null)
            _progressService.Quests = new Quests();
        _progressService.Quests.SetProgress(_progressService.Progress);
    }
}