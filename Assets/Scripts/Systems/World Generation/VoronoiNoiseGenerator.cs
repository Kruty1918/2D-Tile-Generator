using UnityEngine;

namespace Systems.WorldGenerator_
{
    public static class VoronoiNoiseGenerator
    {
        public static float GenerateNoise(float x, float y, float scale)
        {
            Vector2 samplePoint = new Vector2(x, y) / scale;
            Vector2Int samplePointInt = new Vector2Int(Mathf.FloorToInt(samplePoint.x), Mathf.FloorToInt(samplePoint.y));

            float minDistance = float.MaxValue;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Vector2Int gridPoint = samplePointInt + new Vector2Int(i, j);
                    Vector2 pointInCell = gridPoint + new Vector2(0.5f, 0.5f); // Center of the cell
                    float distance = Vector2.Distance(pointInCell, samplePoint);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                }
            }

            // Returning the normalized distance as noise value.
            return Mathf.InverseLerp(0f, 1f, minDistance);
        }
    }
}