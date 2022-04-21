using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.CodeBase.Utils.ObjectPool
{
    public class PoolManager<T> : IPoolManager<T> where T : Component
    {
        private readonly Dictionary<T, IObjectPool<T>> _pools = new Dictionary<T, IObjectPool<T>>();
        private readonly Dictionary<T, IObjectPool<T>> _instancedObjectsPools = new Dictionary<T, IObjectPool<T>>();
        private GameObject _root;

        public void Initialize() => 
            _root = new GameObject { name = "PoolManager" };

        public T SpawnObject(T prefab, Vector3 position, Quaternion rotation)
        {
            if (!_pools.ContainsKey(prefab)) 
                WarmPool(prefab, 1);

            var pool = _pools[prefab];

            var clone = pool.GetFromPool();
            clone.transform.SetPositionAndRotation(position, rotation);
            clone.gameObject.SetActive(true);

            _instancedObjectsPools.Add(clone, pool);
            return clone;
        }

        public void ReleaseObject(T obj)
        {
            obj.gameObject.SetActive(false);

            if (_instancedObjectsPools.ContainsKey(obj))
            {
                _instancedObjectsPools[obj].PushToPool();
                _instancedObjectsPools.Remove(obj);
            }
            else
                Debug.LogWarning("No pool contains the object: " + obj.name);
        }

        public void CleanUp()
        {
            _pools.Clear();
            _instancedObjectsPools.Clear();
        }

        private void WarmPool(T prefab, int size)
        {
            if (_pools.ContainsKey(prefab))
                throw new Exception("Pool for prefab " + prefab.name + " has already been created");

            var pool = new ObjectPool<T>(() => InstantiatePrefab(prefab), size);
            _pools[prefab] = pool;
        }

        private T InstantiatePrefab(T prefab)
        {
            var obj = Object.Instantiate(prefab, _root.transform, true);
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}