using UnityEngine;
using Zenject;
using static RichPanel;

public class SceneContextInstaller : MonoInstaller
{
    [SerializeField] private MergeLevel _mergeLevel;
    [SerializeField] private CreateBuildingPopup createBuildingPopup;
    [SerializeField] private CreateBuildingPopupScroller createBuildingPopupScroller;
    [SerializeField] private District district;
    [SerializeField] private QuestPopup questPopup;
    [SerializeField] private RichPanel richPanel;

    public override void InstallBindings()
    {
        Container.Bind<BuildingProvider>().AsSingle();
        Container.Bind<CreateBuildingPopupPresenter>().AsSingle();
        Container.Bind<QuestsPresenter>().AsSingle();
        Container.Bind<DistrictsPresenter>().AsSingle();
        Container.Bind<CurrencyCreator>().AsSingle();
        Container.Bind<QuestsProvider>().AsSingle();
        Container.Bind<QuestGiver>().AsSingle();
        Container.Bind<BuildingCreator>().AsSingle();
        Container.Bind<RichPresenter>().AsSingle();

        Container.Bind<CreateBuildingPopup>().FromInstance(createBuildingPopup).AsSingle();
        Container.Bind<CreateBuildingPopupScroller>().FromInstance(createBuildingPopupScroller).AsSingle();
        Container.Bind<District>().FromInstance(district).AsSingle();
        Container.Bind<QuestPopup>().FromInstance(questPopup).AsSingle();
        Container.Bind<RichPanel>().FromInstance(richPanel).AsSingle();

        Container.BindInstance<MergeLevel>(_mergeLevel).AsSingle();

        Container.Bind<SlotsManager>().AsSingle();
        Container.Bind<MergeItemsManager>().AsSingle();
        Container.Bind<DraggableItem>().AsSingle();
        Container.Bind<ItemsCatalogue>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InformationPanel>().FromComponentInHierarchy().AsSingle();
    }
}