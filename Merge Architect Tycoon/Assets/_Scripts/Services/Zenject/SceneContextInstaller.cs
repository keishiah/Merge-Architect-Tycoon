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
            Container.Bind<BuildingProvider>().AsSingle();
            Container.Bind<CreateBuildingPopupPresenter>().AsSingle();
            Container.Bind<QuestsPresenter>().AsSingle();

            Container.BindInstance<MergeLevel>(_mergeLevel).AsSingle();
            Container.Bind<SlotsManager>().AsSingle();
            Container.Bind<MergeItemsManager>().AsSingle();
            Container.Bind<DraggableItem>().AsSingle();
            Container.Bind<ItemsCatalogue>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InformationPanel>().FromComponentInHierarchy().AsSingle();
        }
    }
}