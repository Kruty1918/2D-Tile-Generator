using UnityEngine;

namespace Systems.ChunkSystem_
{
    public static class BoundsExtensions
    {
        public static Vector2Int GetChunkDirection(Vector2 point, Bounds bounds)
        {
            // ����� ���
            if (point.x < bounds.min.x && point.y > bounds.max.y) return new Vector2Int(-1, 1);
            if (point.x > bounds.max.x && point.y > bounds.max.y) return new Vector2Int(1, 1);

            // ���� ���
            if (point.x < bounds.min.x && point.y < bounds.min.y) return new Vector2Int(-1, -1);
            if (point.x > bounds.max.x && point.y < bounds.min.y) return new Vector2Int(1, -1);

            // ˳� �� ���� ���
            if (point.x < bounds.min.x) return Vector2Int.left;
            if (point.x > bounds.max.x) return Vector2Int.right;

            // ������ �� ����� ���
            if (point.y < bounds.min.y) return Vector2Int.down;
            else return Vector2Int.up;

        }
    }

}
