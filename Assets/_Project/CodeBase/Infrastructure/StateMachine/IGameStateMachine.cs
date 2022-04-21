namespace _Project.CodeBase.Infrastructure.StateMachine
{
    public interface IGameStateMachine : IGameService
    {
        void ChangeState<T>();
    }
}