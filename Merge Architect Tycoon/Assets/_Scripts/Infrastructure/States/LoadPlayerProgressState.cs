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
        Progress progress = LoadProgressOrInitNew();

        NotifyProgressReaderServices(progress);

        _gameStateMachine.Enter<LoadLevelState, string>(AssetPath.StartGameScene);
    }

    private void NotifyProgressReaderServices(Progress progress)
    {
        foreach (IProgressReader reader in _progressReaderServices)
            reader.LoadProgress(progress);
    }

    public void Exit()
    {
    }

    private Progress LoadProgressOrInitNew()
    {
        _progressService.Progress =
            SaveLoadService.Load<Progress>(SaveKey.Progress);
        if (_progressService.Progress == null)
            _progressService.Progress = new Progress();

        return _progressService.Progress;
    }
}