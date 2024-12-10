using UnityEngine;

namespace Systems
{
    public struct ConvertCoords
    {
        private static float offset = 0.5f;

        public static Vector2Int ConvertToTileCoord(Vector2 worldPos, bool useOffset, float tileWidth = 1f, float tileHeight = 1f)
        {
            int x, y;

            if (useOffset)
            {
                x = Mathf.RoundToInt(worldPos.x / tileWidth + offset);
                y = Mathf.RoundToInt(worldPos.y / tileHeight + offset);
            }
            else
            {
                x = Mathf.RoundToInt(worldPos.x / tileWidth);
                y = Mathf.RoundToInt(worldPos.y / tileHeight);
            }

            return new Vector2Int(x, y);
        }

        public static Vector2 ConvertToWorldPos(Vector2Int tileCoord, float tileWidth = 1f, float tileHeight = 1f)
        {
            return new Vector2(tileCoord.x * tileWidth, tileCoord.y * tileHeight);
        }

    }

}
