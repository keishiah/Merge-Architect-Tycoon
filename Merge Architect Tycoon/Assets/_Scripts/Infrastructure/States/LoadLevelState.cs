using CodeBase.CompositionRoot;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services;
using CodeBase.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPaylodedState<string>
    {
        private IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IPlayerProgressService _playerProgressService;

        private string _sceneName;
        private readonly QuestsPresenter _questsPresenter;

        private readonly SceneContextProvider _sceneContextProvider;


        public LoadLevelState(ISceneLoader sceneLoader, PlayerProgressService playerProgressService,
            SceneContextProvider sceneContextProvider)
        {
            _sceneLoader = sceneLoader;
            _playerProgressService = playerProgressService;
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

            InitLevel();
        }

        private void InitLevel()
        {
            InitializePopupPresenter();
        }

        private void InitializePopupPresenter()
        {
            var createBuildingPopupPresenter = _sceneContextProvider.Resolve<CreateBuildingPopupPresenter>();
            createBuildingPopupPresenter.InitializePresenter();
        }
    }
}