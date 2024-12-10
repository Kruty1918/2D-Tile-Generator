using UnityEngine;

namespace Systems.WorldGenerator_
{
    [System.Serializable]
    public class GenerationInfo
    {
        public Opportunity[] tilePrefabs;
        [Range(0, 1)]    
        public float level;
        [Range(0, 1)]
        public float chance;
    }
}

