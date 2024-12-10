namespace Data.WorldSetup
{
    [System.Serializable]
    public class WorldData
    {
        #region Поля

        public string worldName = "WordlName";
        public int seed;

        public GameMode gameMode;
        public WorldSize worldSize;
        public GameDifficulty difficulty;
        public string createdTime;

        #endregion


        #region Конструктор

        public WorldData(string worldName, int seed, GameMode gameMode, WorldSize worldSize, GameDifficulty difficulty, string createdTime)
        {
            this.worldName = worldName;
            this.seed = seed;
            this.gameMode = gameMode;
            this.worldSize = worldSize;
            this.difficulty = difficulty;
            this.createdTime = createdTime;
        }

        #endregion
    }
}
