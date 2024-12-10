using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    /// <summary>
    /// Базовий клас для кешування Моно поведінки.
    /// </summary>
    public class MonoCache : MonoBehaviour
    {
        public static List<MonoCache> allUpdate = new List<MonoCache>(100);

        private void OnEnable()
        {
            allUpdate.Add(this);
        }

        private void OnDisable()
        {
            allUpdate.Remove(this);
        }

        private void OnDestroy()
        {
            allUpdate.Remove(this);
        }

        public void Tick() => OnTick();

        public virtual void OnTick() { }
    }
}
