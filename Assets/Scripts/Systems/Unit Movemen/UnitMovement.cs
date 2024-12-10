using System.Collections.Generic;
using UnityEngine;
using Mediators;

namespace Systems
{
    public static class UnitMovement
    {
        private static Unit selectUnit;
        private static List<Tile> cachedMovementTiles = new List<Tile>();

        //ßêùî þí³ò äîñòóïíèé äëÿ ðóõó òîä³ âèêîíóºìî ëîã³êó äàë³
        public static void CheckSelectedUnit(Unit selectedUnit)
        {
            if (selectedUnit.HasMovementSteps())
            {
                selectUnit = selectedUnit;
                SetMovementMarks(selectedUnit);
            }
        }

        //ßêùî CanMove == true âèêîíóºòüñÿ ðóõ, ÿêùî í³, â³äì³íÿºìî âèá³ð þí³òó
        public static void CheckSelectedTile(Tile selectedTile)
        {
            MoveUnit(selectedTile);
            DeselectUnit(selectedTile);
        }

        private static void MoveUnit(Tile selectedTile)
        {
            if (CanMoveUnit(selectedTile))
            {
                //Vector2Int direction = CalculateTileDirection.Calculate(selectUnit.Coord, selectedTile.Coord);

                ReplaceUnitInGrid(selectedTile);
                UpdateUnitPosition(selectedTile);
                DestroyExistingMarkers();
            }
        }

        private static bool CanMoveUnit(Tile selectedTile) => selectedTile.CanMove && selectUnit != null;      
        private static void ReplaceUnitInGrid(Tile selectedTile)
        {
            GridSystem.RemoveElement<Unit>(GridId.UnitGrid, selectUnit.Coord);
            selectUnit.Coord = selectedTile.Coord;
            GridSystem.AddElement(GridId.UnitGrid, selectUnit.Coord, selectUnit);
        }
        private static void UpdateUnitPosition(Tile selectedTile) => selectUnit.transform.position = selectedTile.transform.position;


        private static void DeselectUnit(Tile selectedTile)
        {
            if (!GridSystem.ContainsElement<Unit>(GridId.UnitGrid, selectedTile.Coord) && cachedMovementTiles.Count > 0 && !selectedTile.CanMove)
            {
                DestroyExistingMarkers();
                ResetMovementTiles();
                selectUnit = null;
            }
        }

        private static void SetMovementMarks(Unit unit)
        {
            if (cachedMovementTiles.Count != 0) ResetMovementTiles();

            List<Tile> neighbourTiles =
            FindNeighbourElements.Find(GridSystem.GetAllElement<Tile>(GridId.TileGrid), unit.Coord, unit.movementRadius, false);

            DestroyExistingMarkers();

            foreach (Tile tile in neighbourTiles)
            {
                if (!TileHasUnit(tile.Coord) && CheckTileLayer(unit, tile))
                {
                    SM.Instance<MarkerSystem>().SpawnMark(SM.Instance<MarkerSystem>().MovementMark, tile.transform.position);
                    tile.CanMove = true;
                    cachedMovementTiles.Add(tile);
                }
            }
        }

        private static void ResetMovementTiles()
        {
            foreach(Tile tile in cachedMovementTiles)
            {
                tile.CanMove = false;
            }

            cachedMovementTiles.Clear();
        }
        
        private static void DestroyExistingMarkers() => SM.Instance<MarkerSystem>().DestroyExistingMarkers();
        private static bool TileHasUnit(Vector2Int coordToCheck) => GridSystem.ContainsElement<Unit>(GridId.UnitGrid, coordToCheck);
        private static bool CheckTileLayer(Unit unit, Tile tile) => tile.CompareLayers(unit.MovementLayers);

    }
}


