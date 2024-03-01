using _Scripts.Logic.Merge;
using _Scripts.Logic.Merge.Items;
using _Scripts.Logic.Merge.MergePlane;
using _Scripts.UI.Presenters;
using UnityEngine;
using Zenject;

namespace _Scripts.Services.Zenject
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] private MergeLevel _mergeLevel;

        public override void InstallBindings()
        {
            Container.Bind<BuildingProvider>().AsSingle();
            Container.Bind<CreateBuildingPopupPresenter>().AsSingle();
            Container.Bind<QuestsPresenter>().AsSingle();
            Container.Bind<DistricsSwitcher>().AsSingle();

            Container.BindInstance<MergeLevel>(_mergeLevel).AsSingle();
            Container.Bind<SlotsManager>().AsSingle();
            Container.Bind<MergeItemsManager>().AsSingle();
            Container.Bind<DraggableItem>().AsSingle();
            Container.Bind<ItemsCatalogue>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InformationPanel>().FromComponentInHierarchy().AsSingle();
        }
    }
}