using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Services;
using CodeBase.Services.SceneContextProvider;
using CodeBase.Services.StaticDataService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly SceneContextProvider _sceneContextProvider;
        private GameObject _heroGameobject;
        private readonly List<IPool> _poolsList = new();

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService,
            SceneContextProvider sceneContextProvider)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _sceneContextProvider = sceneContextProvider;
        }

        public async UniTask WarmUp()
        {
        }


        public Transform CreateEmptyPoolParent(string name)
        {
            var newChild = new GameObject(name);
            var newChildTransform = newChild.transform;
            return newChildTransform;
        }

        public async UniTask CreatePool<T>(int count, string assetPath, string parentName)
            where T : MonoBehaviour, IPoolElement
        {
            var pool = new Pooler<T>(this, assetPath);
            _poolsList.Add(pool);
            await pool.CreatePool(count, parentName);
        }

        public async UniTask<GameObject> GetPoolElement<T>(Vector3 position) where T : MonoBehaviour, IPoolElement
        {
            var pool = GetPool<T>();
            var view = await pool.GetFreeElement();
            view.transform.position = position;
            return view;
        }

        public async UniTask<T> CreateIPoolElement<T>(string path, Transform parent = null) where T : IPoolElement
        {
            var prefab = await _assetProvider.Load<GameObject>(path);
            var element = _sceneContextProvider.GetCurrentSceneContextInstantiator().InstantiatePrefab(prefab, parent)
                .GetComponent<T>();

            return element;
        }

        public void DeactivatePoolObject<T>(T element) where T : MonoBehaviour, IPoolElement
        {
            element.transform.SetParent(GetPoolsParent<T>());
            element.gameObject.SetActive(false);
        }

        private IPool GetPool<T>() where T : MonoBehaviour, IPoolElement
        {
            var type = typeof(T);
            foreach (var pool in _poolsList)
            {
                if (pool.GetPoolType() == type)
                    return pool;
            }

            return default;
        }

        private Transform GetPoolsParent<T>() where T : MonoBehaviour, IPoolElement => GetPool<T>().GetPoolsParent();

        public void Cleanup()
        {
            _poolsList.Clear();
            _assetProvider.Cleanup();
        }
    }
}