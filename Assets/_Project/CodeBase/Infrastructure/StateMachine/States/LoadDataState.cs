using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.UI;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.StateMachine.States
{
    public class LoadDataState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IGameStateMachine _stateMachine;

        public LoadDataState(
            IGameFactory gameFactory, 
            IUIFactory uiFactory,
            IGameStateMachine stateMachine)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _stateMachine = stateMachine;
        }

        public async void Enter()
        {
            await LoadPrefabs();
            await LoadUI();
            _stateMachine.ChangeState<GameState>();
        }

        private async UniTask LoadPrefabs() => 
            await _gameFactory.LoadPrefabs();
        
        private async UniTask LoadUI()
        {
            await _uiFactory.LoadUIRoot();
            await _uiFactory.LoadHud();
            await _uiFactory.LoadDeathWindow();
        }

        public void Exit() { }
    }
}