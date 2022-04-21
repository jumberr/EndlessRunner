using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.CodeBase.Logic.Obstacle
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2 _groundPosition;
        [SerializeField] private Vector2 _ceilingPosition;
        [SerializeField] private float _speed;
        [SerializeField] private float _amount;
        [SerializeField] private float _gapMin;
        [SerializeField] private float _gapMax;

        private Obstacle _obstaclePrefab;
        private List<Obstacle> _obstacles = new List<Obstacle>();

        public void Construct(Obstacle obstaclePrefab)
        {
            _obstaclePrefab = obstaclePrefab;
            FirstInit();
        }

        private void OnDisable() => 
            _obstacles.Clear();

        public void ReuseObstacle(Obstacle obstacle)
        {
            var startPos = _groundPosition;
            startPos.y = YPosition();
            startPos.x += RandomSign() * Gap();
            ChangePosition(obstacle, startPos);
        }

        private void FirstInit()
        {
            var pos = _groundPosition;

            for (var i = 0; i < _amount; i++)
            {
                pos.y = YPosition();
                var obstacle = ConstructObstacle(pos, _speed);
                pos.x += Gap();
                _obstacles.Add(obstacle);
            }
        }

        private Obstacle ConstructObstacle(Vector3 position, float speed)
        {
            var obstacle = CreateObstacle(position);
            obstacle.SetSpeed(speed);
            return obstacle;
        }

        private Obstacle CreateObstacle(Vector3 position) => 
            Instantiate(_obstaclePrefab, position, Quaternion.identity);
        
        private void ChangePosition(Obstacle obstacle, Vector3 position) => 
            obstacle.transform.position = position;

        private float YPosition() => 
            Random.Range(0, 2) % 2 == 0 ? _ceilingPosition.y : _groundPosition.y;

        private float Gap() => 
            Random.Range(_gapMin, _gapMax);

        private int RandomSign()
            => Random.Range(0, 2) % 2 == 0 ? -1 : 1;
    }
}