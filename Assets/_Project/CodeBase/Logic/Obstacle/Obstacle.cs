using UnityEngine;

namespace _Project.CodeBase.Logic.Obstacle
{
    public class Obstacle : MonoBehaviour
    {
        private float _speed;

        private void Update() => 
            Move();

        public void SetSpeed(float speed) => 
            _speed = speed;

        private void Move() => 
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}