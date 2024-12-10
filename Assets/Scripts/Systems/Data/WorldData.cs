using UnityEngine;

[System.Serializable]
public class WorldData
{
    public string worldName = "WordlName";
    public int seed;

    public GameMode gameMode;
    public WorldSize worldSize;
    public GameDifficulty difficulty;
    public string createdTime;

    public WorldData(string worldName, int seed, GameMode gameMode, WorldSize worldSize, GameDifficulty difficulty, string createdTime)
    {
        this.worldName = worldName;
        this.seed = seed;
        this.gameMode = gameMode;
        this.worldSize = worldSize;
        this.difficulty = difficulty;
        this.createdTime = createdTime;
    }

    public WorldData() { }
}

[System.Serializable]
public class ChunkData
{
    int x;
    int y;

    public Vector2Int position
    {
        get {  return new Vector2Int(x, y);}
        set { x = value.x; y = value.y; }
    }
}