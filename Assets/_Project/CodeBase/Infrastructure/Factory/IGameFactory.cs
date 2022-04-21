using _Project.CodeBase.Logic.Obstacle;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IGameService
    {
        UniTask LoadPrefabs();
        GameObject CreatePlayer();
        void CreateCamera();
        void CreateObstacleGenerator();
        void CreateObstacleReuser();
    }
}