using System;
using UnityEngine;

namespace Mediators.Singleton
{
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingletonMediator<T> where T : class, new()
    {
        private static T instance;
        private bool isDublicate;

        T ISingletonMediator<T>.Instance
        {
            get { return instance; }
        }

        protected virtual void Awake()
        {
            try
            {
                if (SM.HasSingleton<T>())
                {
                    Debug.LogError($"[MonoSingleton] Помилка: Singleton типу {typeof(T)} вже існує. Знищення дубліката.");
                    isDublicate = true;
                    Destroy(gameObject);
                }
                else
                {
                    instance = this as T;
                    SM.RegisterSingleton(instance);
                    Debug.Log($"[MonoSingleton] Singleton типу {typeof(T)} зареєстровано успішно.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[MonoSingleton] Помилка під час Awake для Singleton {typeof(T)}: {ex.Message}");
            }
        }

        protected virtual void OnDestroy()
        {
            try
            {
                if (instance != null && !isDublicate)
                {
                    SM.UnregisterSingleton<T>();
                    Debug.Log($"[MonoSingleton] Singleton типу {typeof(T)} скасовано успішно.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[MonoSingleton] Помилка під час OnDestroy для Singleton {typeof(T)}: {ex.Message}");
            }
        }
    }
}