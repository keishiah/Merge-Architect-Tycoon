using CodeBase.CompositionRoot;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.SceneContextProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPaylodedState<string>
    {
        private IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISceneContextProvider _sceneContextProvider;
        private readonly IPlayerProgressService _playerProgressService;

        private string _sceneName;


        public LoadLevelState(ISceneLoader sceneLoader, ISceneContextProvider
                sceneContextProvider,
            IPlayerProgressService playerProgressService)
        {
            _sceneLoader = sceneLoader;
            _sceneContextProvider = sceneContextProvider;
            _playerProgressService = playerProgressService;
        }

        public void Enter(string sceneName)
        {

            _sceneLoader.Load(sceneName, OnLoaded);
            _sceneName = sceneName;
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
            _sceneContextProvider.SetCurrentSceneContext(_sceneName);
            _sceneContextProvider.Resolve<SceneObjectsProvider>().InitializeSceneObjects();

            await InitLevel();
        }

        private async UniTask InitLevel()
        {
            // await CreateUi();
            await CreatePools();
        }


        private async UniTask CreatePools()
        {
        }
    }
}