using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public struct FindNeighbourElements
    {
        public static List<T> Find<T>(Dictionary<Vector2Int, T> grid, Vector2Int centerCoord, int radius, bool returnCenterObject)
        {
            List<T> neighborObjects = new List<T>();
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    if (x == 0 && y == 0 && !returnCenterObject) continue;

                    Vector2Int neighbor = new Vector2Int(centerCoord.x + x, centerCoord.y + y);

                    if (grid.ContainsKey(neighbor)) neighborObjects.Add(grid[neighbor]);
                }
            }
            return neighborObjects;
        }

    }
}


