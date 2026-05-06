using System;
using System.Reflection;
using UnityEngine;

namespace SubmarineJourney.Core.DI {
    public static class DependencyInjector {
        public static void Inject(object aTarget) {
            var type = aTarget.GetType();
            
            // Procházení polí
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields) {
                if (field.IsDefined(typeof(InjectAttribute), true)) {
                    var service = ServiceRegistry.Get(field.FieldType);
                    if (service != null) {
                        field.SetValue(aTarget, service);
                    } else {
                        Debug.LogWarning($"[DI] Could not resolve dependency for field: {field.Name} in {type.Name}");
                    }
                }
            }

            // Procházení properties
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var prop in props) {
                if (prop.IsDefined(typeof(InjectAttribute), true)) {
                    var service = ServiceRegistry.Get(prop.PropertyType);
                    if (service != null) {
                        prop.SetValue(aTarget, service);
                    } else {
                        Debug.LogWarning($"[DI] Could not resolve dependency for property: {prop.Name} in {type.Name}");
                    }
                }
            }
        }
    }
}
