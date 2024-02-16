using UnityEngine;
using Zenject;

public class ManagersInstaller : MonoInstaller
{
    [SerializeField]
    private MergeLevel _mergeLevel;

    public override void InstallBindings()
    {
        Container.BindInstance<MergeLevel>(_mergeLevel).AsSingle();
        Container.Bind<SlotsManager>().AsSingle();
        Container.Bind<MergeItemsManager>().AsSingle();
    }
}
