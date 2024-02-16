using CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factories
{
    public interface IGameFactory
    {
        void Cleanup();
        UniTask WarmUp();
        Transform CreateEmptyPoolParent(string name);
        UniTask<T> CreateIPoolElement<T>(string path, Transform parent = null) where T : IPoolElement;

        UniTask CreatePool<T>(int count, string assetPath, string parentName)
            where T : MonoBehaviour, IPoolElement;

        UniTask<GameObject> GetPoolElement<T>(Vector3 position) where T : MonoBehaviour, IPoolElement;
        void DeactivatePoolObject<T>(T element) where T : MonoBehaviour, IPoolElement;
    }
}