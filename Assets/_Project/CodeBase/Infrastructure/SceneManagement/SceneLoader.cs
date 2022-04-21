using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(string initial, Action onComplete = null)
        {
            await SceneManager.LoadSceneAsync(initial);
            onComplete?.Invoke();
        }
    }
}