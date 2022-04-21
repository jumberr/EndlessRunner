using UnityEngine;

namespace _Project.CodeBase.Utils.ObjectPool
{
    public interface IObjectPool<T> where T : Component
    {
        T GetFromPool();
        void PushToPool();
    }
}