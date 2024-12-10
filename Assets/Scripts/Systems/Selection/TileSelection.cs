using UnityEngine;

namespace Systems
{
    public static class TileSelection
    {
        public static void ConvertInputPosition(Vector2 playerClickPosition)
        {          
            CheckTile(ConvertCoords.ConvertToTileCoord(playerClickPosition, true));
        }

        private static void CheckTile(Vector2Int tileCoord)
        {
            Tile selectTile = GridSystem.GetElement<Tile>(GridId.TileGrid, tileCoord);

            if (selectTile != null) SystemEvents.TileSelected(selectTile);
        }
    }
}


