using System;
using System.Collections.Generic;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSceneLoader();

        BindGameStateMachine();

        BindStaticDataService();

        BindPlayerProgressService();

        BindGameTutorial();

        BindGameStates();

        BindStateFactory();

        BindAssetProvider();

        BindUiPresenter();

        BindBuilder();

        BindSceneContextProvider();

        BindFirebaseLogger();
    }

    private void BindFirebaseLogger()
    {
        Container.Bind<FirebaseLogger>().AsSingle();
    }

    private void BindSceneContextProvider()
    {
        Container.Bind<SceneContextProvider>().AsSingle();
    }

    private void BindGameTutorial()
    {
        IEnumerable<IProgressReader> readers = new IProgressReader[]
        {
            new TutorialReader(),
        };

        Container.BindInstance(readers).AsSingle();
    }

    private void BindBuilder()
    {
    }

    private void BindUiPresenter()
    {
        Container.BindInterfacesAndSelfTo<CreateBuildingPopupPresenter>().AsSingle();
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
        Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

    private void BindAssetProvider()
    {
        Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
    }

    private void BindPlayerProgressService()
    {
        Container.Bind<PlayerProgressService>().AsSingle();
        Container.Bind<PlayerProgress>().AsSingle();
    }

    private void BindSceneLoader() =>
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

    private void BindGameStateMachine()
    {
        Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
    }
}