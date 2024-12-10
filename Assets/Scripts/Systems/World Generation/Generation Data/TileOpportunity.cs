using UnityEngine;

namespace Systems.WorldGenerator_
{
    [System.Serializable]
    public struct Opportunity
    {
        public GameObject prefab;
        public Texture2D texture;
        [Range(0, 1)]
        public float weight;
    }
}

