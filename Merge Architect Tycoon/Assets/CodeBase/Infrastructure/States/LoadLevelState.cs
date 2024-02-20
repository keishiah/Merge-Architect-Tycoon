using CodeBase.CompositionRoot;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services.PlayerProgressService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPaylodedState<string>
    {
        private IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IPlayerProgressService _playerProgressService;

        private string _sceneName;


        public LoadLevelState(ISceneLoader sceneLoader, PlayerProgressService playerProgressService)
        {
            _sceneLoader = sceneLoader;
            _playerProgressService = playerProgressService;
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

        private async void OnLoaded()
        {
            await InitLevel();
        }

        private async UniTask InitLevel()
        {
        }
    }
}