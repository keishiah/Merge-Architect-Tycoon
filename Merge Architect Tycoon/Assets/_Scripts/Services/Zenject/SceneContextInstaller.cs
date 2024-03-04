using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneContextInstaller : MonoInstaller
{
    [SerializeField] private MergeLevel _mergeLevel;
    [SerializeField] private CreateBuildingPopup createBuildingPopup;
    [SerializeField] private CreateBuildingPopupScroller createBuildingPopupScroller;
    [SerializeField] private District district;

    public override void InstallBindings()
    {
        Container.Bind<BuildingProvider>().AsSingle();
        Container.Bind<CreateBuildingPopupPresenter>().AsSingle();
        Container.Bind<QuestsPresenter>().AsSingle();
        Container.Bind<DistrictsPresenter>().AsSingle();
        Container.Bind<CurrencyCreator>().AsSingle();
        Container.Bind<CreateBuildingPopup>().FromInstance(createBuildingPopup).AsSingle();
        Container.Bind<CreateBuildingPopupScroller>().FromInstance(createBuildingPopupScroller).AsSingle();
        Container.Bind<District>().FromInstance(district).AsSingle();

        Container.BindInstance<MergeLevel>(_mergeLevel).AsSingle();
        Container.Bind<SlotsManager>().AsSingle();
        Container.Bind<MergeItemsManager>().AsSingle();
        Container.Bind<DraggableItem>().AsSingle();
        Container.Bind<ItemsCatalogue>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InformationPanel>().FromComponentInHierarchy().AsSingle();
    }
}