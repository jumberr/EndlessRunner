using System;
using _Project.CodeBase.Constants;
using UnityEngine;

namespace _Project.CodeBase.Logic.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        public event Action OnDeath;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(TagConstants.Obstacle)) return;
            Time.timeScale = 0f;
            OnDeath?.Invoke();
        }
    }
}