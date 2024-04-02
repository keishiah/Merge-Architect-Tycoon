﻿public class LoadLevelState : IPaylodedState<string>
{
    private string _sceneName;

    private readonly SceneContextProvider _sceneContextProvider;

    private readonly ISceneLoader _sceneLoader;
    private readonly AudioPlayer _audioPlayer;

    public LoadLevelState(ISceneLoader sceneLoader,
        SceneContextProvider sceneContextProvider, AudioPlayer audioPlayer)
    {
        _sceneLoader = sceneLoader;
        _sceneContextProvider = sceneContextProvider;
        _audioPlayer = audioPlayer;
    }

    public void SetGameStateMachine(IGameStateMachine gameStateMachine){}
    public void Enter(string sceneName)
    {
        _sceneName = sceneName;

        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
    }

    private void OnLoaded()
    {
        _sceneContextProvider.SetCurrentSceneContext(_sceneName);
        InitSettings();
        InitLevel();
    }

    private void InitSettings()
    {
        _audioPlayer.InitializeAudioPlayer();
        _sceneContextProvider.Resolve<AudioSettingsPresenter>().OnSceneLoaded();
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
        _sceneContextProvider.Resolve<RichPresenter>().OnSceneLoaded();
        _sceneContextProvider.Resolve<BuildingProvider>().OnSceneLoaded();
        _sceneContextProvider.Resolve<DistrictsPresenter>().OnSceneLoaded();
        _sceneContextProvider.Resolve<TruckProvider>().OnSceneLoaded();
    }

}