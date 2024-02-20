using CodeBase.CompositionRoot;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.SceneContextProvider;
using CodeBase.UI.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPaylodedState<string>
    {
        private IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly ISceneContextProvider _sceneContextProvider;
        private readonly IPlayerProgressService _playerProgressService;

        private string _sceneName;


        public LoadLevelState(ISceneLoader sceneLoader, ISceneContextProvider
                sceneContextProvider, IGameFactory gameFactory, IUIFactory uiFactory,
            IPlayerProgressService playerProgressService)
        {
            _sceneLoader = sceneLoader;
            _sceneContextProvider = sceneContextProvider;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _playerProgressService = playerProgressService;
        }

        public void Enter(string sceneName)
        {
            _sceneName = sceneName;

            _gameFactory.Cleanup();
            _uiFactory.Cleanup();

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
            _sceneContextProvider.SetCurrentSceneContext(_sceneName);
            _sceneContextProvider.Resolve<SceneObjectsProvider>().InitializeSceneObjects();

            await InitLevel();
            await _gameFactory.WarmUp();
            await _uiFactory.WarmUp();
        }

        private async UniTask InitLevel()
        {
            await CreateUi();
            await CreatePools();
        }

        private async UniTask CreateUi()
        {
            await _uiFactory.CreateuiRoot();
            await _uiFactory.CreateBuildingPopup();
            await _uiFactory.CreateCoinsUi();
        }

        private async UniTask CreatePools()
        {
        }
    }
}