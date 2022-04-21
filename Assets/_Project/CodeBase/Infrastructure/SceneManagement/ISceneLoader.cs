using System;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.SceneManagement
{
    public interface ISceneLoader : IGameService
    {
        UniTask LoadSceneAsync(string initial, Action onComplete = null);
    }
}