using Systems.GridSystem_;
using Systems.ChunkSystem_;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public struct GridSystem
    {
        #region Initialization

        private static Dictionary<GridId, object> grids = new Dictionary<GridId, object>
        {
            {GridId.TileGrid, new Grid<Tile>()},
            {GridId.UnitGrid, new Grid<Unit>()},
            {GridId.ChunkGrid, new Grid<Chunk>()}
        };

        #endregion

        public static void AddElement<T>(GridId Id, Vector2Int elementCoord, T elementToAdd)
        {
            ((Grid<T>)grids[Id]).AddElement(elementCoord, elementToAdd);
        }

        public static void RemoveElement<T>(GridId Id, Vector2Int elementCoord)
        {
            ((Grid<T>)grids[Id]).RemoveElement(elementCoord);
        }

        public static T GetElement<T>(GridId Id, Vector2Int elementCoord)
        {
            if (!((Grid<T>)grids[Id]).ContainsElement(elementCoord)) return default;

            return ((Grid<T>)grids[Id]).GetElement(elementCoord);
        }

        public static bool ContainsElement<T>(GridId Id, Vector2Int elementCoord)
        {
            return ((Grid<T>)grids[Id]).ContainsElement(elementCoord);
        }

        public static Dictionary<Vector2Int, T> GetAllElement<T>(GridId Id)
        {
            return ((Grid<T>)grids[Id]).Elements;
        }

        public static void ClearGridElements<T>(GridId Id)
        {
            ((Grid<T>)grids[Id]).ClearElements();
        }
    }

    #region Grid Id

    public enum GridId
    {
        TileGrid,
        UnitGrid,
        ChunkGrid
    }

    #endregion
}

