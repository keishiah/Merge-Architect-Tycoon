using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class SceneContextInstaller : MonoInstaller
{
    [SerializeField] private MergeLevel _mergeLevel;
    [SerializeField] private CreateBuildingPopup createBuildingPopup;
    [SerializeField] private CreateBuildingPopupScroller createBuildingPopupScroller;
    [SerializeField] private CitiesMapPopup citiesMapPopup;
    [SerializeField] private QuestPanel questPopup;
    [SerializeField] private TruckPanel truckPanel;
    [SerializeField] private RichPanel richPanel;
    [SerializeField] private SettingsPanel settingsPanel;
    [SerializeField] private EffectsPresenter effectsPresenter;
    [SerializeField] private CameraZoomer cameraZoomer;
    [SerializeField] private BackGroundButton backGroundButton;
    [SerializeField] private Cities cities;

    public override void InstallBindings()
    {
        Container.Bind<BuildingProvider>().AsSingle();
        Container.Bind<TruckProvider>().AsSingle();
        Container.Bind<CreateBuildingPopupPresenter>().AsSingle();
        Container.Bind<QuestsPresenter>().AsSingle();
        Container.Bind<DistrictsPresenter>().AsSingle();
        Container.Bind<CurrencyCreator>().AsSingle();
        Container.Bind<QuestsProvider>().AsSingle();
        Container.Bind<BuildingCreator>().AsSingle();
        Container.Bind<RichPresenter>().AsSingle();
        Container.Bind<AudioSettingsPresenter>().AsSingle();

        Container.Bind<CreateBuildingPopup>().FromInstance(createBuildingPopup).AsSingle();
        Container.Bind<CreateBuildingPopupScroller>().FromInstance(createBuildingPopupScroller).AsSingle();
        Container.Bind<EffectsPresenter>().FromInstance(effectsPresenter).AsSingle();
        Container.Bind<CitiesMapPopup>().FromInstance(citiesMapPopup).AsSingle();
        Container.Bind<QuestPanel>().FromInstance(questPopup).AsSingle();
        Container.Bind<TruckPanel>().FromInstance(truckPanel).AsSingle();
        Container.Bind<RichPanel>().FromInstance(richPanel).AsSingle();
        Container.Bind<SettingsPanel>().FromInstance(settingsPanel).AsSingle();
        Container.Bind<CameraZoomer>().FromInstance(cameraZoomer).AsSingle();
        Container.Bind<BackGroundButton>().FromInstance(backGroundButton).AsSingle();
        Container.Bind<Cities>().FromInstance(cities).AsSingle();

        Container.BindInstance<MergeLevel>(_mergeLevel).AsSingle();

        Container.Bind<SlotsManager>().AsSingle();
        Container.Bind<DraggableItem>().AsSingle();
        Container.Bind<ItemsCatalogue>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InformationPanel>().FromComponentInHierarchy().AsSingle();
    }
}