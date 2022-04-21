using _Project.CodeBase.Constants;
using UnityEngine;

namespace _Project.CodeBase.Logic.Obstacle
{
    public class ObstacleReuser : MonoBehaviour
    {
        private ObstacleGenerator _generator;

        public void Construct(ObstacleGenerator generator) => 
            _generator = generator;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagConstants.Obstacle))
                _generator.ReuseObstacle(other.GetComponent<Obstacle>());
        }
    }
}