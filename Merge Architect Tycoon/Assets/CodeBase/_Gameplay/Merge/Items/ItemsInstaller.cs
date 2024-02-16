using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ItemInstallerSO", menuName = "Installers/ItemInstallerSO")]
public class ItemsInstaller : ScriptableObjectInstaller<ItemsInstaller>
{

    [SerializeField] private List<MergeItem> testSOList;       // List of ScriptableObjects that need injection. Find the installer SO (this script) and drop the SOs that need injection here.

    public override void InstallBindings()
    {
       // Container.Bind<SlotsManager>().AsSingle();      // Bind the classes that are needed in the ScriptableObjects to this container (I used a class TestToInject to test that injection worked)

        foreach (var scriptableObject in testSOList)
        {
            Container.QueueForInject(scriptableObject);     // Use QueueForInject to inject all the dependences that ScriptableObjects require
        }

    }
}
