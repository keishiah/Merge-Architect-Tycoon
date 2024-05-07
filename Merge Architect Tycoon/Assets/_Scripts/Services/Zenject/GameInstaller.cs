using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Multiline(10)]
    public string WARNING = "Global Bindings Using Project Context\r\n" +
        "This all works great for each individual scene, \r\n" +
        "but what if you have dependencies that you wish to persist permanently across all scenes?\r\n" +
        " In Zenject you can do this by adding installers to a ProjectContext object.\r\n\r\n" +
        "To do this, first you need to create a prefab for the ProjectContext, and then you can add installers to it.\r\n" +
        " You can do this most easily by selecting the menu item Edit -> Zenject -> Create Project Context.\r\n" +
        " You should then see a new asset in the folder \bAssets/Resources\b called 'ProjectContext'.\r\n" +
        " Alternatively, you can right click somewhere in Projects tab and select Create -> Zenject -> ProjectContext.";

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
        Container.Bind<AudioPlayer>().FromComponentInNewPrefabResource(AssetPath.AudioPlayer).AsSingle();
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