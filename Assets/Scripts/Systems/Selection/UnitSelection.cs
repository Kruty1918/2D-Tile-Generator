using UnityEngine;

namespace Systems
{
    public static class UnitSelection
    {
        public static void CheckSelectedTile(Tile selectedTile)
        {
            Vector2Int unitCoord = selectedTile.Coord;
            if (GridSystem.GetElement<Unit>(GridId.UnitGrid, unitCoord)) CanSelectUnit(GridSystem.GetElement<Unit>(GridId.UnitGrid, unitCoord));
        }

        public static void CanSelectUnit(Unit selectedUnit)
        {
            SystemEvents.UnitSelected(selectedUnit);
        }
    }
}