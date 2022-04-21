using System;
using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.SceneManagement;
using _Project.CodeBase.Infrastructure.StateMachine.States;
using _Project.CodeBase.UI;

namespace _Project.CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private IState _actual;
        private readonly Dictionary<Type, IState> _states;

        public GameStateMachine(
            ISceneLoader sceneLoader,
            IUIFactory uiFactory,
            IGameFactory gameFactory)
        {
            _states = new Dictionary<Type, IState>
            {
                {typeof(BootState), new BootState(sceneLoader, this)},
                {typeof(LoadDataState), new LoadDataState(gameFactory, uiFactory, this)},
                {typeof(GameState), new GameState(sceneLoader, this, gameFactory, uiFactory)}
            };
        }

        public void ChangeState<T>()
        {
            _actual?.Exit();
            _actual = GetState<T>();
            _actual.Enter();
        }

        private IState GetState<T>() => 
            _states[typeof(T)];
    }
}