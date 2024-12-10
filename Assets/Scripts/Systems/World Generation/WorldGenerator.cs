using Mediators.Singleton;
using System.Collections.Generic;
using Systems.ChunkSystem_;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.WorldGenerator_
{
    public class WorldGenerator : MonoSingleton<WorldGenerator>
    {
        [SerializeField] private GenerationInfo[] tilePrefabs;
        [SerializeField] private GenerationInfo[] envPrefabs;

        [SerializeField] private NoiseMapSettings _nms;

        [SerializeField] private int minimumValidNoiseCount = 10;
        [SerializeField] private int maximumIterations = 3;
        [SerializeField] private float minimumValidNoiseValue = 0.5f;

        private INoiseMapGenerator noiseMapGenerator;
        private IObjectGenerator<GenerationInfo> objectGenerator = new ObjectGenerator<GenerationInfo>();
        private List<Texture2D> createdTiles = new List<Texture2D>();
        private List<GameObject> createdEnvironmentObjects = new List<GameObject>();

        private float[,] noiseMap;

        protected override void Awake()
        {
            base.Awake();
            _nms.seed = Random.Range(int.MinValue, int.MaxValue);
            noiseMapGenerator = new NoiseMap();
        }

        /// <summary>
        /// Генерує об'єкти в області, визначеній межами.
        /// </summary>
        /// <param name="chunkBounds">Межі області, в якій необхідно сгенерувати об'єкти.</param>
        /// <param name="chunkSize">Розмір одного блоку, на який розбивається область.</param>
        /// <returns>Список сгенерованих об'єктів.</returns>
        public GenerationResult Generation(Bounds chunkBounds, int chunkSize)
        {
            // Якщо повинен бути створений блок з землею, генеруємо карту шуму
            if (ShouldSpawnGroundedChunk())
            {
                GenerateNoiseMap(chunkSize, chunkBounds);
            }
            else
            {
                // Інакше генеруємо шумову карту за бужеми параметрами
                noiseMap = noiseMapGenerator.GenerateMap(_nms, chunkBounds, chunkSize);
            }

            // Список сгенерованих об'єктів
            List<GameObject> generatedObjects = new List<GameObject>();
            Dictionary<Vector2Int, Texture2D> tiles = new Dictionary<Vector2Int, Texture2D>();

            // Центр області генерації
            Vector2Int centerChunkBounds = new Vector2Int(Mathf.FloorToInt(chunkBounds.center.x), Mathf.FloorToInt(chunkBounds.center.y));

            tiles = CreateWorldTiles(chunkSize, noiseMap, tilePrefabs);

            // Розташовуємо об'єкти на тайлах
            generatedObjects.AddRange(PlaceObjectsOnTiles(centerChunkBounds, chunkSize, noiseMap, envPrefabs));

            GenerationResult result = new GenerationResult(tiles, generatedObjects);

            return result;
        }

        private bool ShouldSpawnGroundedChunk()
        {
            return GridSystem.GetAllElement<Chunk>(GridId.ChunkGrid).Count == 1;
        }

        private Dictionary<Vector2Int, Texture2D> CreateWorldTiles(int chunkSize, float[,] noiseMap, GenerationInfo[] tilePrefabs)
        {
            if (createdTiles.Count > 0) createdTiles.Clear();
            var resultTiles = new Dictionary<Vector2Int, Texture2D>();

            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    Texture2D tile = objectGenerator.GenerateTextureObject(noiseMap[x, y], tilePrefabs);

                    if (tile != null)
                    {
                        resultTiles.Add(new Vector2Int(x, y), tile);
                    }
                }
            }

            return resultTiles;
        }

        private List<GameObject> PlaceObjectsOnTiles(Vector2Int centerChunkBounds, int chunkSize, float[,] noiseMap, GenerationInfo[] envPrefabs)
        {
            if (createdEnvironmentObjects.Count > 0) createdEnvironmentObjects.Clear();

            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    GenerationInfo generationInfo = GetTileInfoByNoise(noiseMap[x, y], envPrefabs, true, true);
                    GameObject otherObject = objectGenerator.GenerateObject(noiseMap[x, y], envPrefabs, true, true);
                    if (otherObject != null && Random.value < generationInfo.chance)
                    {
                        SpawnObject(createdEnvironmentObjects, otherObject, centerChunkBounds, x, y, chunkSize);
                    }
                }
            }

            return createdEnvironmentObjects;
        }

        private void SpawnObject(List<GameObject> result, GameObject otherObject, Vector2Int chunkBounds, int x, int y, int chunkSize)
        {
            Vector2 objectPosition = new Vector2(chunkBounds.x + x - chunkSize / 2 + 0.5f, chunkBounds.y + y - chunkSize / 2 + 0.5f);
            GameObject instantiatedObject = Instantiate(otherObject, objectPosition, Quaternion.identity);
            result.Add(instantiatedObject);
        }

        private GenerationInfo GetTileInfoByNoise(float n, GenerationInfo[] prefabs, bool returnNull = false, bool reverse = false)
        {
            foreach (var tile in prefabs)
            {
                if (IsNoiseLevelMatch(n, tile.level, reverse))
                {
                    return tile;
                }
            }

            if (returnNull) return null;

            return prefabs[prefabs.Length - 1];
        }

        private bool IsNoiseLevelMatch(float noise, float level, bool reverse)
        {
            return reverse ? noise >= level : noise < level;
        }

        private void GenerateNoiseMap(int chunkSize, Bounds chunkBounds)
        {
            _nms.seed = Random.Range(int.MinValue, int.MaxValue);
            noiseMapGenerator = new NoiseMap();

            bool validNoiseGenerated = false;
            int iterationCount = 0;

            while (!validNoiseGenerated)
            {
                float[,] noise = noiseMapGenerator.GenerateMap(_nms, chunkBounds, chunkSize);
                int validNoiseCount = CountValidNoise(noise);
                if (validNoiseCount >= minimumValidNoiseCount)
                {
                    validNoiseGenerated = true;
                    noiseMap = noise;
                }
                else
                {
                    iterationCount++;
                    if (iterationCount > maximumIterations)
                    {
                        Debug.LogError("Could not generate valid noise after " + maximumIterations + " attempts.");
                        break;
                    }
                    // Shift the offset
                    _nms.offset += new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
                }
            }
        }

        private int CountValidNoise(float[,] noise)
        {
            int validNoiseCount = 0;
            int width = noise.GetLength(0);
            int height = noise.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (noise[x, y] > minimumValidNoiseValue)
                    {
                        validNoiseCount++;
                    }
                }
            }

            return validNoiseCount;
        }
    }
}