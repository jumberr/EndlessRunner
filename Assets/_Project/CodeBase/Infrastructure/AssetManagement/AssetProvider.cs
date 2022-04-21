using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace _Project.CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public async UniTask<T> LoadAssetAsync<T>(string address) => 
            await Addressables.LoadAssetAsync<T>(address);

        public async UniTask<T> InstantiateAsync<T>(string address) where T : Component => 
            Object.Instantiate(await LoadAssetAsync<T>(address));

        public async UniTask<T> InstantiateAsync<T>(string address, Vector3 position) where T : Component => 
            Object.Instantiate(await LoadAssetAsync<T>(address), position, Quaternion.identity);

        public async UniTask<T> InstantiateAsync<T>(string address, Transform parent) where T : Component => 
            Object.Instantiate(await LoadAssetAsync<T>(address), parent);

        public async UniTask<T> InstantiateAsync<T>(string address, Vector3 position, Quaternion rotation) where T : Component => 
            Object.Instantiate(await LoadAssetAsync<T>(address), position, rotation);
    }
}