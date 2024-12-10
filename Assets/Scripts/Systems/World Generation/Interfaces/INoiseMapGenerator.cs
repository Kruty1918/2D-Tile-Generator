using UnityEngine;

namespace Systems.WorldGenerator_
{
    // Інтерфейс для генерації шумової карти
    public interface INoiseMapGenerator
    {
        float[,] GenerateMap(NoiseMapSettings nms, Bounds chunkBounds, int noiseSize);

    }
}

