using _Scripts.Infrastructure;
using _Scripts.Infrastructure.AssetManagment;
using _Scripts.Infrastructure.Factories;
using _Scripts.Infrastructure.States;
using _Scripts.Logic;
using _Scripts.UI.Presenters;
using Zenject;

namespace _Scripts.Services.Zenject
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneLoader();

            BindGameStateMachine();

            BindStaticDataService();

            BindPlayerProgressService();

            BindGameStates();

            BindStateFactory();
            
            BindAssetProvider();

            BindUiPresenter();

            BindBuilder();
            Container.Bind<SceneContextProvider>().AsSingle();
        }

        private void BindBuilder()
        {
            Container.Bind<BuildingCreator>().AsSingle();
        }

        private void BindUiPresenter()
        {
            Container.BindInterfacesAndSelfTo<UiPresenter>().AsSingle();
            // Container.BindInterfacesAndSelfTo<CreateBuildingPopupPresenter>().AsSingle();
        }

        private void BindStateFactory()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.Bind<LoadPlayerProgressState>().AsSingle();
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
        }

        private void BindStaticDataService() =>
            Container.BindInterfacesAndSelfTo<StaticDataService.StaticDataService>().AsSingle();

        private void BindAssetProvider()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void BindPlayerProgressService()
        {
            Container.Bind<Progress>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerProgressService.PlayerProgressService>().AsSingle();
        }

        private void BindSceneLoader() =>
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }
    }
}