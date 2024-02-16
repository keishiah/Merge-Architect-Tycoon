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
            Container.BindInstance<MergeLevel>(_mergeLevel).AsSingle();
            Container.Bind<SlotsManager>().AsSingle();
            Container.Bind<MergeItemsManager>().AsSingle();
        }
    }
}