using Mediators.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class MarkerSystem : MonoSingleton<MarkerSystem>
    {    
        private List<GameObject> existingMarkers = new List<GameObject>();

        [Header("Marker GameObjects")]
        [SerializeField] private GameObject selectMark;

        [Space(10)]

        [Header("Marker Prefabs")]
        [SerializeField] private GameObject movementMark;
        [SerializeField] private GameObject attackMark;

        public GameObject MovementMark { get { return movementMark; } }
      
        public void SpawnMark(GameObject markPrefab, Vector2 markPosition)
        {
            GameObject newMark = Instantiate(markPrefab, markPosition, Quaternion.identity);
            existingMarkers.Add(newMark);
        }

        public void SetSelectMark(Tile selectedTile)
        {
            ActiveMark(selectMark, true);
            selectMark.transform.position = selectedTile.transform.position;
        }

        public void ActiveMark(GameObject markToActive, bool mode)
        {
            if (markToActive.activeSelf != mode) markToActive.SetActive(mode);
        }

        public void DestroyExistingMarkers()
        {
            if (existingMarkers.Count <= 0) return;

            for (int i = 0; i < existingMarkers.Count; i++)
            {
                Destroy(existingMarkers[i]);
            }
        }
    }
}

