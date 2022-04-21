using _Project.CodeBase.Infrastructure.StateMachine;
using _Project.CodeBase.Infrastructure.StateMachine.States;
using _Project.CodeBase.Logic.Player;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows
{
    public class DeathWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private Button _restart;
        private IGameStateMachine _stateMachine;
        private PlayerDeath _playerDeath;

        public void Construct(IGameStateMachine stateMachine, PlayerDeath death)
        {
            _stateMachine = stateMachine;
            _playerDeath = death;
            Subscribe();
            _window.SetActive(false);
        }

        private void OnDisable() => 
            UnSubscribe();

        private void Subscribe()
        {
            _playerDeath.OnDeath += OnDeath;
            _restart.onClick.AddListener(Restart);
        }

        private void UnSubscribe()
        {
            _playerDeath.OnDeath -= OnDeath;
            _restart.onClick.RemoveListener(Restart);
        }

        private void OnDeath() => 
            _window.SetActive(true);

        private void Restart() => 
            _stateMachine.ChangeState<GameState>();
    }
}