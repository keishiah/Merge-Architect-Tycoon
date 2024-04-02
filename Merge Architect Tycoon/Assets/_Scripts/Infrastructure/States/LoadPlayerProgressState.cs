using System.Collections.Generic;

public class LoadPlayerProgressState : IState
{
    private IGameStateMachine _gameStateMachine;
    private readonly IEnumerable<IProgressReader> _progressReaderServices;
    private readonly PlayerProgress _progressService;
    private ApplicationSettings _settings;

    public LoadPlayerProgressState(PlayerProgress progressService, 
        ApplicationSettings settings,
        IEnumerable<IProgressReader> progressReaderServices)
    {
        _progressService = progressService;
        _settings = settings;
        _progressReaderServices = progressReaderServices;
    }

    public void SetGameStateMachine(IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
        LoadSettingsOrInitNew();

        LoadProgressOrInitNew();

        NotifyProgressReaderServices();

        _gameStateMachine.Enter<LoadLevelState, string>(AssetPath.StartGameScene);
    }

    private void LoadSettingsOrInitNew()
    {
        _settings.Audio = SaveLoadService.Load<AudioSettings>(SaveKey.SoundSettings);
        if( _settings.Audio == null )
            _settings.Audio = new AudioSettings();
    }

    private void LoadProgressOrInitNew()
    {
        _progressService.Riches =
            SaveLoadService.Load<PlayerRiches>(SaveKey.Riches);
        if (_progressService.Riches == null)
            _progressService.Riches = new PlayerRiches();

        _progressService.Buldings =
            SaveLoadService.Load<BuildingsData>(SaveKey.Buldings);
        if (_progressService.Buldings == null)
            _progressService.Buldings = new BuildingsData();

        _progressService.Quests = 
            SaveLoadService.Load<QuestsData>(SaveKey.Quests);
        if (_progressService.Quests == null)
            _progressService.Quests = new QuestsData();

        _progressService.Tutorial =
            SaveLoadService.Load<TutorialData>(SaveKey.Tutorial);
        if (_progressService.Tutorial == null)
            _progressService.Tutorial = new TutorialData();

        _progressService.Trucks =
            SaveLoadService.Load<TrucksData>(SaveKey.Truck);
        if (_progressService.Trucks == null)
            _progressService.Trucks = new TrucksData();
    }

    private void NotifyProgressReaderServices()
    {
        foreach (IProgressReader reader in _progressReaderServices)
            reader.LoadProgress(_progressService);
    }

    public void Exit()
    {
    }

}