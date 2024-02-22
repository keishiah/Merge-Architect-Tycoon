using CodeBase.CompositionRoot;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services.PlayerProgressService;
using CodeBase.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPaylodedState<string>
    {
        private IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly UiPresenter _uiPresenter;

        private string _sceneName;


        public LoadLevelState(ISceneLoader sceneLoader, PlayerProgressService playerProgressService,
            UiPresenter uiPresenter)
        {
            _sceneLoader = sceneLoader;
            _playerProgressService = playerProgressService;
            _uiPresenter = uiPresenter;
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
            InitLevel();
        }

        private void InitLevel()
        {
        }
    }
}