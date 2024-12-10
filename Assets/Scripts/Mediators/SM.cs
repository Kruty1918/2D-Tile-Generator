using Mediators.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mediators
{
    /// <summary>
    /// Singleton Mediator
    /// </summary>
    public static class SM
    {
        private static Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        public static T Instance<T>() where T : class, ISingletonMediator<T>, new()
        {
            var type = typeof(T);
            if (!_instances.ContainsKey(type))
            {
                Debug.LogError($"Екземпляр типу {type} не знайдено. Переконайтеся, що ви його створили та зареєстрували перед спробою доступу до нього.");
                return null;
            }

            return (T)_instances[type];
        }

        internal static void RegisterSingleton<T>(T instance) where T : class
        {
            var type = typeof(T);
            if (!_instances.ContainsKey(type))
            {
                _instances[type] = instance;
            }
            else
            {
                Debug.LogWarning($"Сінглтон типу {type} вже зареєстровано. Пропускаємо реєстрацію.");
            }
        }

        internal static void UnregisterSingleton<T>() where T : class
        {
            var type = typeof(T);
            if (_instances.ContainsKey(type))
            {
                _instances.Remove(type);
            }
            else
            {
                Debug.LogWarning($"Сінглтон типу {type} не знайдено. Пропускаємо скасування реєстрації.");
            }
        }

        public static bool HasSingleton<T>() where T : class
        {
            var type = typeof(T);
            bool hasSingleton = _instances.ContainsKey(type);

            if (!hasSingleton)
            {
                Debug.LogWarning($"Сінглтон типу {type} не знайдено.");
            }

            return hasSingleton;
        }
    }
}
