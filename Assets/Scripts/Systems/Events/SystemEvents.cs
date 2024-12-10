using System;
using UnityEngine;

namespace Systems
{
    /// <summary>
    /// Статичний клас, який визначає події для різних системних взаємодій у грі.
    /// </summary>
    public struct SystemEvents
    {
        public static event Action<Vector2> playerClick;
        public static event Action<Tile> tileSelected;
        public static event Action<Unit> unitSelected;

        public static void PlayerClick(Vector2 clickPos) => playerClick?.Invoke(clickPos);
        public static void TileSelected(Tile selectedTile) => tileSelected?.Invoke(selectedTile);
        public static void UnitSelected(Unit selectedUnit) => unitSelected?.Invoke(selectedUnit);
    }
}
