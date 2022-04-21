using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.StateMachine;
using _Project.CodeBase.Logic.Player;
using _Project.CodeBase.UI.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.CodeBase.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        private GameObject _uiRootPrefab;
        private GameObject _hudPrefab;
        private DeathWindow _deathWindowPrefab;
        
        private Transform _uiRoot;
        private GameObject _hud;
        private DeathWindow _deathWindow;

        public UIFactory(
            IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask LoadUIRoot() => 
            _uiRootPrefab = await _assetProvider.LoadAssetAsync<GameObject>(AssetPath.UIRoot);

        public async UniTask LoadHud() => 
            _hudPrefab = await _assetProvider.LoadAssetAsync<GameObject>(AssetPath.Hud);

        public async UniTask LoadDeathWindow() => 
            _deathWindowPrefab = (await _assetProvider.LoadAssetAsync<GameObject>(AssetPath.DeathWindow))
                .GetComponent<DeathWindow>();

        public void CreateUIRoot() => 
            _uiRoot = Object.Instantiate(_uiRootPrefab).transform;

        public void CreateHud() => 
            _hud = Object.Instantiate(_hudPrefab, _uiRoot);

        public void CreateDeathWindow(IGameStateMachine stateMachine, PlayerDeath playerDeath)
        {
            _deathWindow = Object.Instantiate(_deathWindowPrefab, _uiRoot);
            _deathWindow.Construct(stateMachine, playerDeath);
        }
    }
}