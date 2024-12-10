using UnityEngine;

namespace Utilities
{
    public struct CalculateTileDirection
    {
        public static Vector2Int Calculate(Vector2Int coordA, Vector2Int coordB)
        {
            if (coordB.y > coordA.y && coordB.x == coordA.x) return Vector2Int.up;
            if (coordB.y < coordA.y && coordB.x == coordA.x) return Vector2Int.down;

            if (coordB.y == coordA.y && coordB.x > coordA.x) return Vector2Int.right;
            if (coordB.y == coordA.y && coordB.x < coordA.x) return Vector2Int.left;

            if (coordB.y > coordA.y && coordB.x > coordA.x) return new Vector2Int(1, 1);
            if (coordB.y > coordA.y && coordB.x < coordA.x) return new Vector2Int(-1, 1);

            if (coordB.y < coordA.y && coordB.x > coordA.x) return new Vector2Int(1, -1);
            else return new Vector2Int(-1, -1);
        }
    }
}

