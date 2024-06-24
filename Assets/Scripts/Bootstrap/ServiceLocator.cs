using System;
using System.Collections.Generic;

namespace Bootstrap
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static void RegisterService<T>(T service)
        {
            var type = typeof(T);
            _services.TryAdd(type, service);
        }

        public static T GetService<T>()
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return (T)service;
            }
            
            throw new Exception($"Service of type {type} not registered.");
        }
    }
}