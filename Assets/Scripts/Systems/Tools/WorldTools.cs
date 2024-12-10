//using Gameframe.SaveLoad;
//using Gameframe.SaveLoad;
using UnityEngine;

public struct WorldTools
{
    private static Vector2Int smallWorldSize = new Vector2Int(60, 60);
    private static Vector2Int averageWorldSize = new Vector2Int(120, 120);
    private static Vector2Int bigWorldSize = new Vector2Int(240, 240);
    private static Vector2Int endlessWorldSize = new Vector2Int(480, 480);


    public static Vector2Int SmallWorldSize { get { return smallWorldSize; } }
    public static Vector2Int AverageWorldSize { get { return averageWorldSize; } }
    public static Vector2Int BigWorldSize { get { return bigWorldSize; } }
    public static Vector2Int EndlessWorldSize { get { return endlessWorldSize; } }


    ////public static bool HasSameWorld(WorldData worldData)
    ////{
    ////    return !SaveLoadUtility.Exists(GetKey(worldData));
    ////}

    public static string GetKey(WorldData worldData)
    {
        return worldData.worldName + worldData.seed + worldData.gameMode + worldData.worldSize + worldData.difficulty;
    }

    public static Vector2Int GetWorldSize(WorldData worldData) 
    {
        if (worldData == null)
        {
            Debug.LogError($"Wrold Data {worldData} не існує!");
            return Vector2Int.zero;
        }

        WorldSize worldSize = worldData.worldSize;

        switch (worldSize)
        {
            case WorldSize.Small:
                return smallWorldSize;
            case WorldSize.Average:
                return averageWorldSize;
            case WorldSize.Big:
                return bigWorldSize;
            case WorldSize.Endless:
                return endlessWorldSize;
            default:
                return Vector2Int.zero;
        }
    }
}