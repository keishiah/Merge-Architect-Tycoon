using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Services
{
    public class Pooler<T> : IPool where T : MonoBehaviour, IPoolElement
    {
        private List<GameObject> _pool;
        private Type _type;

        private readonly string _path;
        private Transform parent;
        private readonly IGameFactory _gameFactory;

        public Pooler(IGameFactory gameFactory, string path)
        {
            _gameFactory = gameFactory;
            _path = path;
        }

        public async UniTask CreatePool(int count, string name)
        {
            _pool = new List<GameObject>();
            parent = _gameFactory.CreateEmptyPoolParent(name);
            for (int i = 0; i < count; i++)
            {
                await CreateObject();
            }
        }

        public Transform GetPoolsParent() => parent;

        private async UniTask<GameObject> CreateObject(
            bool isActiveByDefault = false)
        {
            T createdObject = await _gameFactory.CreateIPoolElement<T>(_path, parent);

            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject.gameObject);
            return createdObject.gameObject;
        }

        private bool HasFreeElement(out GameObject element)
        {
            foreach (var unit in _pool)
            {
                if (!unit.gameObject.activeInHierarchy)
                {
                    element = unit;
                    unit.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public async UniTask<GameObject> GetFreeElement()
        {
            if (HasFreeElement(out var element))
                return element;
            return await CreateObject(true);
        }

        public Type GetPoolType()
        {
            _type = typeof(T);
            return _type;
        }

        public void DeactivateAllPoolUnits()
        {
            foreach (var unit in _pool)
            {
                unit.gameObject.SetActive(false);
            }
        }
    }

    public interface IPoolElement
    {
    }

    public interface IPool
    {
        UniTask<GameObject> GetFreeElement();
        Type GetPoolType();
        Transform GetPoolsParent();
    }
}