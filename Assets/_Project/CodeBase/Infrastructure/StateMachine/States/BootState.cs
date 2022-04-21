using _Project.CodeBase.Constants;
using _Project.CodeBase.Infrastructure.SceneManagement;

namespace _Project.CodeBase.Infrastructure.StateMachine.States
{
    public class BootState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;

        public BootState(
            ISceneLoader sceneLoader, 
            IGameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            await _sceneLoader.LoadSceneAsync(SceneConstants.Initial);
            _gameStateMachine.ChangeState<LoadDataState>();
        }

        public void Exit() { }
    }
}