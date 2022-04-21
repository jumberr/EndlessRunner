using _Project.CodeBase.Infrastructure;
using _Project.CodeBase.Infrastructure.StateMachine;
using _Project.CodeBase.Logic.Player;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.UI
{
    public interface IUIFactory : IGameService
    {
        UniTask LoadUIRoot();
        UniTask LoadHud();
        UniTask LoadDeathWindow();
        
        void CreateUIRoot();
        void CreateHud();
        void CreateDeathWindow(IGameStateMachine stateMachine, PlayerDeath playerDeath);
    }
}