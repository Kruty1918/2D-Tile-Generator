using UnityEngine;

namespace Systems.GlobalMono
{
    public class GlobalMono : MonoBehaviour
    {
        private void Update()
        {
            for (int i = 0; i < MonoCache.allUpdate.Count; i++)
            {
                MonoCache.allUpdate[i].Tick();
            }
        }
    }
}