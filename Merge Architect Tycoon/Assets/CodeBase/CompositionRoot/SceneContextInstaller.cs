using CodeBase.Services;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] private MergeLevel _mergeLevel;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneObjectsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<CreateBuildingPopupPresenter>().AsSingle();
            
            Container.BindInstance<MergeLevel>(_mergeLevel).AsSingle();
            Container.Bind<SlotsManager>().AsSingle();
            Container.Bind<MergeItemsManager>().AsSingle();
            Container.Bind<ItemsCatalogue>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InformationPanel>().FromComponentInHierarchy().AsSingle();
        }
    }
}