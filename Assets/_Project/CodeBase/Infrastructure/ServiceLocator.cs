using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure
{
    public class ServiceLocator
    {
        private static ServiceLocator _locator;
        private readonly Dictionary<Type, IGameService> _services = new Dictionary<Type, IGameService>();
        
        public static ServiceLocator Current => _locator ??= new ServiceLocator();
        
        public T Get<T>() where T : IGameService
        {
            var key = typeof(T);
            if (_services.ContainsKey(key)) return (T) _services[key];
            Debug.LogError($"{key} not registered with {GetType().Name}");
            throw new InvalidOperationException();
        }
        
        public void Register<T>(IGameService service) where T : IGameService
        {
            var key = typeof(T);
            if (_services.ContainsKey(key))
            {
                Debug.LogError($"Service of type {key} is already registered with the {GetType().Name}.");
                return;
            }
            _services.Add(key, service);
        }

        public void Unregister<T>() where T : IGameService
        {
            var key = typeof(T);
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"Service of type {key} is not registered with the {GetType().Name}.");
                return;
            }
            _services.Remove(key);
        }
    }
}