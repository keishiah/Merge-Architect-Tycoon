using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.SaveLoadService;
using CodeBase.Services.StaticDataService;
using CodeBase.UI;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneLoader();

            BindGameStateMachine();

            BindStaticDataService();

            BindPlayerProgressService();

            BindSaveLoadService();

            BindInputService();

            BindGameStates();

            BindStateFactory();
            
            BindAssetProvider();

            BindUiPresenter();

            BindBuilder();
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

        private void BindInputService()
        {
            Container.BindInterfacesAndSelfTo<MobileInputService>()
                .AsSingle();
        }

        private void BindGameStates()
        {
            Container.Bind<LoadPlayerProgressState>().AsSingle();
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
        }

        private void BindStaticDataService() =>
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

        private void BindSaveLoadService()
        {
            Container
                .BindInterfacesAndSelfTo<SaveLoadService>()
                .AsSingle();
        }

        private void BindAssetProvider()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void BindPlayerProgressService()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerProgressService>()
                .AsSingle();
        }


        private void BindSceneLoader() =>
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }
    }
}