using Zenject;

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
        Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

    private void BindAssetProvider()
    {
        Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
    }

    private void BindPlayerProgressService()
    {
        Container.Bind<Progress>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerProgressService>().AsSingle();
    }

    private void BindSceneLoader() =>
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

    private void BindGameStateMachine()
    {
        Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
    }
}