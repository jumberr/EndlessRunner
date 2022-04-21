using System;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.SceneManagement;
using _Project.CodeBase.Infrastructure.StateMachine;
using _Project.CodeBase.Infrastructure.StateMachine.States;
using _Project.CodeBase.UI;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake() => 
            DontDestroyOnLoad(this);

        private void Start()
        {
            RegisterServices();
            EnterBootState();
        }

        private void RegisterServices()
        {
            var locator = ServiceLocator.Current;
            locator.Register<ISceneLoader>(new SceneLoader());
            locator.Register<IAssetProvider>(new AssetProvider());
            locator.Register<IGameFactory>(new GameFactory(locator.Get<IAssetProvider>()));
            locator.Register<IUIFactory>(new UIFactory(locator.Get<IAssetProvider>()));
            locator.Register<IGameStateMachine>(new GameStateMachine(locator.Get<ISceneLoader>(),
                locator.Get<IUIFactory>(), locator.Get<IGameFactory>()));
        }

        private void EnterBootState() =>
            ServiceLocator.Current.Get<IGameStateMachine>().ChangeState<BootState>();
    }
}
