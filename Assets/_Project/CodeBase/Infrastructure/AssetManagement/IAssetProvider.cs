using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IGameService
    {
        UniTask<T> LoadAssetAsync<T>(string address);
        UniTask<T> InstantiateAsync<T>(string address) where T : Component;
        UniTask<T> InstantiateAsync<T>(string address, Vector3 position) where T : Component;
        UniTask<T> InstantiateAsync<T>(string address, Transform parent) where T : Component;
        UniTask<T> InstantiateAsync<T>(string address,  Vector3 position, Quaternion rotation) where T : Component;
    }
}