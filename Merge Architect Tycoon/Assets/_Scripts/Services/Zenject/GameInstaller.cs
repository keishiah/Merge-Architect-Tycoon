using System.Collections.Generic;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSceneLoader();

        BindGameStateMachine();

        BindStateFactory();

        BindGameStates();

        BindStaticDataService();

        BindApplicationSettings();

        BindPlayerProgressService();

        BindGameTutorial();

        BindAssetProvider();

        BindUiPresenter();

        BindBuilder();

        BindSceneContextProvider();

        BindFirebaseLogger();

        BindAudioPlayer();
    }


    private void BindAudioPlayer()
    {
        Container.Bind<AudioPlayer>().FromComponentInNewPrefabResource("Prefabs/AudioPlayer").AsSingle();
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
        Container.Bind<TutorialReader>().AsSingle();
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
    private void BindApplicationSettings()
    {
        Container.Bind<ApplicationSettings>().AsSingle();
        Container.Bind<AudioServise>().AsSingle();
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