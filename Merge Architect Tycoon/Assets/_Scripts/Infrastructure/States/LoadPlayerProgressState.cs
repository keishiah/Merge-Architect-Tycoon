using System.Collections.Generic;

public class LoadPlayerProgressState : IState
{
    private IGameStateMachine _gameStateMachine;
    private readonly IEnumerable<IProgressReader> _progressReaderServices;
    private readonly IPlayerProgressService _progressService;

    public LoadPlayerProgressState(IPlayerProgressService progressService,
        IEnumerable<IProgressReader> progressReaderServices)
    {
        _progressService = progressService;
        _progressReaderServices = progressReaderServices;
    }

    public void SetGameStateMachine(IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();

        NotifyProgressReaderServices();

        _gameStateMachine.Enter<LoadLevelState, string>(AssetPath.StartGameScene);
    }

    private void LoadProgressOrInitNew()
    {
        _progressService.Progress =
            SaveLoadService.Load<Progress>(SaveKey.Progress);
        if (_progressService.Progress == null)
            _progressService.Progress = new Progress();

        _progressService.Quests = 
            SaveLoadService.Load<Quests>(SaveKey.Quests);
        if (_progressService.Quests == null)
            _progressService.Quests = new Quests();
        _progressService.Quests.SetProgress(_progressService.Progress);

        _progressService.Progress.Tutorial =
            SaveLoadService.Load<TutorialData>(SaveKey.Tutorial);
        if (_progressService.Progress.Tutorial == null)
            _progressService.Progress.Tutorial = new TutorialData();
    }
    private void NotifyProgressReaderServices()
    {
        foreach (IProgressReader reader in _progressReaderServices)
            reader.LoadProgress(_progressService.Progress);
    }

    public void Exit()
    {
    }

}