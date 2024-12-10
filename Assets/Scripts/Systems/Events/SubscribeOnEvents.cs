using Mediators;
using UnityEngine;

namespace Systems.Events
{
    public class SubscribeOnEvents : MonoBehaviour
    {
        private void Start()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            SystemEvents.playerClick += TileSelection.ConvertInputPosition;
            SystemEvents.tileSelected += SM.Instance<MarkerSystem>().SetSelectMark;
            SystemEvents.tileSelected += UnitSelection.CheckSelectedTile;
            SystemEvents.tileSelected += UnitMovement.CheckSelectedTile;
            SystemEvents.unitSelected += UnitMovement.CheckSelectedUnit;
        }
    }
}
