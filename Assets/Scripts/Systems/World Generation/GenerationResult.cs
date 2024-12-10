using System.Collections.Generic;
using UnityEngine;

namespace Systems.WorldGenerator_
{
    public class GenerationResult
    {
        public readonly Dictionary<Vector2Int, Texture2D> Tiles;
        public readonly List<GameObject> EnvironmentObjects;

        public GenerationResult(Dictionary<Vector2Int, Texture2D> tiles, List<GameObject> environmentObjects)
        {
            Tiles = tiles;
            EnvironmentObjects = environmentObjects;
        }
    }
}