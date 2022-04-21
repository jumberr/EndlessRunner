using _Project.CodeBase.Constants;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.SceneManagement;
using _Project.CodeBase.Logic.Player;
using _Project.CodeBase.UI;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.StateMachine.States
{
    public class GameState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public GameState(
            ISceneLoader sceneLoader,
            IGameStateMachine stateMachine,
            IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public async void Enter()
        {
            await _sceneLoader.LoadSceneAsync(SceneConstants.Game);
            CreateWorld(out var player);
            var playerDeath = player.GetComponent<PlayerDeath>();
            CreateUI(playerDeath);
        }

        private void CreateWorld(out GameObject player)
        {
            Time.timeScale = 1;
            _gameFactory.CreateCamera();
            player = _gameFactory.CreatePlayer();
            _gameFactory.CreateObstacleGenerator();
            _gameFactory.CreateObstacleReuser();
        }

        private void CreateUI(PlayerDeath playerDeath)
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateHud();
            _uiFactory.CreateDeathWindow(_stateMachine, playerDeath);
        }

        public void Exit() { }
    }
}