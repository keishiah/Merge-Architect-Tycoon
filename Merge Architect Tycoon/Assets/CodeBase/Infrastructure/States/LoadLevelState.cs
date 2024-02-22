using System;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.SceneContextProvider;
using CodeBase.UI;
using CodeBase.UI.Elements;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPaylodedState<string>
    {
        private IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly UiPresenter _uiPresenter;

        private string _sceneName;
        private readonly QuestsPresenter _questsPresenter;

        private readonly SceneContextProvider _sceneContextProvider;


        public LoadLevelState(ISceneLoader sceneLoader, PlayerProgressService playerProgressService,
            UiPresenter uiPresenter, SceneContextProvider sceneContextProvider)
        {
            _sceneLoader = sceneLoader;
            _playerProgressService = playerProgressService;
            _uiPresenter = uiPresenter;
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
            _sceneContextProvider.Resolve<QuestsPresenter>().InitializeWidget();
        }

        private void InitLevel()
        {
        }
    }
}