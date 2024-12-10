using UnityEngine;

namespace Systems.InputSystem
{
    public class InputSystem : MonoCache
    {
        public override void OnTick()
        {
            if (Input.GetMouseButtonDown(0)) SystemEvents.PlayerClick(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}