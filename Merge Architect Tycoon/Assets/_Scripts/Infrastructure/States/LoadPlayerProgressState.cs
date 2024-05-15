using UnityEngine;

public class LoadPlayerProgressState : IState
{
    private IGameStateMachine _gameStateMachine;
    private readonly PlayerProgress _progressService;
    private ApplicationSettings _settings;

    public LoadPlayerProgressState(PlayerProgress progressService, 
        ApplicationSettings settings)
    {
        _progressService = progressService;
        _settings = settings;
    }

    public void SetGameStateMachine(IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
        LoadSettingsOrInitNew();

        LoadProgressOrInitNew();

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

        _progressService.Tutorial =
            SaveLoadService.Load<TutorialData>(SaveKey.Tutorial);
        if (_progressService.Tutorial == null)
            _progressService.Tutorial = new TutorialData();
        else if (!_progressService.Tutorial.IsComplite && _progressService.Tutorial.StepIndex > 0)
        {
            PlayerPrefs.DeleteAll();
            _progressService.Tutorial = new TutorialData();
            _progressService.Riches = new PlayerRiches();
            _progressService.Buldings = new BuildingsData();
            _progressService.Quests = new QuestsData();
            _progressService.Trucks = new TrucksData();
            _progressService.Inventory = new InventoryData();
            _progressService.PlayerStats = new PlayerStats();
            _progressService.DistrictData = new DistrictData();
            return;
        }

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

        _progressService.Trucks =
            SaveLoadService.Load<TrucksData>(SaveKey.Truck);
        if (_progressService.Trucks == null)
            _progressService.Trucks = new TrucksData();

        _progressService.Inventory =
            SaveLoadService.Load<InventoryData>(SaveKey.Inventory);
        if (_progressService.Inventory == null)
            _progressService.Inventory = new InventoryData();

        _progressService.PlayerStats =
            SaveLoadService.Load<PlayerStats>(SaveKey.Stats);
        if (_progressService.PlayerStats == null)
            _progressService.PlayerStats = new PlayerStats();

        _progressService.DistrictData =
            SaveLoadService.Load<DistrictData>(SaveKey.Districts);
        if (_progressService.DistrictData == null)
            _progressService.DistrictData = new DistrictData();
    }

    public void Exit()
    {
    }

}