using _Project.CodeBase.Constants;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Logic.Obstacle;
using _Project.CodeBase.Utils.ObjectPool;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        
        private GameObject _playerPrefab;
        private GameObject _cameraPrefab;
        private ObstacleGenerator _obstacleGeneratorPrefab;
        private ObstacleReuser _obstacleReuserPrefab;
        private Obstacle _obstaclePrefab;

        private ObstacleGenerator _obstacleGenerator;
        
        public GameFactory(
            IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask LoadPrefabs()
        {
            _playerPrefab = await _assetProvider.LoadAssetAsync<GameObject>(AssetPath.Player);
            _cameraPrefab = await _assetProvider.LoadAssetAsync<GameObject>(AssetPath.Camera);
            _obstacleGeneratorPrefab = (await _assetProvider.LoadAssetAsync<GameObject>(AssetPath.ObstacleGenerator))
                .GetComponent<ObstacleGenerator>();
            _obstacleReuserPrefab = (await _assetProvider.LoadAssetAsync<GameObject>(AssetPath.ObstacleReuser))
                .GetComponent<ObstacleReuser>();
            _obstaclePrefab = (await _assetProvider.LoadAssetAsync<GameObject>(AssetPath.Obstacle))
                .GetComponent<Obstacle>();
        }

        public void CreateObstacleGenerator()
        {
            _obstacleGenerator = Object.Instantiate(_obstacleGeneratorPrefab);
            _obstacleGenerator.Construct(_obstaclePrefab);
        }

        public void CreateObstacleReuser()
        {
            var reuser = Object.Instantiate(_obstacleReuserPrefab, PositionConstants.ReuserPosition, Quaternion.identity);
            reuser.Construct(_obstacleGenerator);
        }

        public GameObject CreatePlayer() =>
            Object.Instantiate(_playerPrefab);

        public void CreateCamera() =>
            Object.Instantiate(_cameraPrefab);
    }
}