using _Scripts.Services;
using _Scripts.Services.PlayerProgressService;
using _Scripts.UI.Presenters;

namespace _Scripts.Infrastructure.States
{
    public class LoadLevelState : IPaylodedState<string>
    {
        private string _sceneName;
        private readonly QuestsPresenter _questsPresenter;
        private readonly UiPresenter _uiPresenter;

        private readonly SceneContextProvider _sceneContextProvider;

        private IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IPlayerProgressService _playerProgressService;

        public LoadLevelState(ISceneLoader sceneLoader, PlayerProgressService playerProgressService,
            SceneContextProvider sceneContextProvider, UiPresenter uiPresenter)
        {
            _sceneLoader = sceneLoader;
            _playerProgressService = playerProgressService;
            _sceneContextProvider = sceneContextProvider;
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
            _sceneContextProvider.SetCurrentSceneContext(_sceneName);
            InitLevel();
        }

        private void InitLevel()
        {
            _sceneContextProvider.Resolve<QuestsPresenter>().InitializeWidget();
            _uiPresenter.InitializeElementsOnSceneLoaded();
            _sceneContextProvider.Resolve<DistrictsPresenter>().OnSceneLoaded();
            InitializePopupPresenter();
        }

        private void InitializePopupPresenter()
        {
            var createBuildingPopupPresenter = _sceneContextProvider.Resolve<CreateBuildingPopupPresenter>();
            createBuildingPopupPresenter.InitializePresenter();
            _sceneContextProvider.Resolve<BuildingProvider>().OnSceneLoaded();
        }
    }
}