using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ItemInstallerSO", menuName = "Installers/ItemInstallerSO")]
public class ItemsInstaller : ScriptableObjectInstaller<ItemsInstaller>
{

    // List of ScriptableObjects that need injection.
    // Find the installer SO (this script) and drop the SOs that need injection here.
    [SerializeField] private List<MergeItem> testSOList;

    public override void InstallBindings()
    {
        // Bind the classes that are needed in the ScriptableObjects to this container
        // (I used a class TestToInject to test that injection worked)
        // Container.Bind<SlotsManager>().AsSingle();

        foreach (var scriptableObject in testSOList)
        {
            // Use QueueForInject to inject all the dependences that ScriptableObjects require
            Container.QueueForInject(scriptableObject);
        }

    }
}
