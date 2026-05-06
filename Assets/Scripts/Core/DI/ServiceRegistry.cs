using System;
using System.Collections.Generic;
using UnityEngine;

namespace SubmarineJourney.Core.DI {
    public static class ServiceRegistry {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void Register<T>(T aService) {
            var type = typeof(T);
            if (!services.ContainsKey(type)) {
                services.Add(type, aService);
                Debug.Log($"[DI] Registered service: {type.Name}");
            }
        }

        public static void Unregister<T>() {
            var type = typeof(T);
            if (services.ContainsKey(type)) {
                services.Remove(type);
                Debug.Log($"[DI] Unregistered service: {type.Name}");
            }
        }

        public static object Get(Type aType) {
            if (services.TryGetValue(aType, out var service)) {
                return service;
            }
            return null;
        }

        public static T Get<T>() where T : class {
            return Get(typeof(T)) as T;
        }
    }
}
