using UnityEngine;

namespace _Project.CodeBase.Utils.ObjectPool
{
    public interface IPoolManager<T> where T : Component
    {
        void Initialize();
        T SpawnObject(T prefab, Vector3 position, Quaternion rotation);
        void ReleaseObject(T clone);
        void CleanUp();
    }
}