using UnityEngine;

public class BackgroundActivator : MonoBehaviour
{
    private void Start()
    {
        Initialized();
    }

    public void Initialized()
    {
        Vector2Int worldSize = WorldTools.GetWorldSize(GameSettings.World);

        bool active = !(worldSize.x > WorldTools.SmallWorldSize.x || worldSize.y > WorldTools.SmallWorldSize.y);

        gameObject.SetActive(active);
    }
}