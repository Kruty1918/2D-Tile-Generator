using System.Linq;
using UnityEngine;

namespace Systems.WorldGenerator_
{
    public class ObjectGenerator<T> : IObjectGenerator<T> where T : GenerationInfo
    {
        public GameObject GenerateObject(float noise, T[] prefabs, bool returnNull = false, bool reverse = false)
        {
            return GetPrefabOrTexture(noise, prefabs, returnNull, reverse, false) as GameObject;
        }

        public Texture2D GenerateTextureObject(float noise, T[] prefabs, bool returnNull = false, bool reverse = false)
        {
            return GetPrefabOrTexture(noise, prefabs, returnNull, reverse, true) as Texture2D;
        }

        private Object GetPrefabOrTexture(float noise, T[] prefabs, bool returnNull, bool reverse, bool useTexture)
        {
            foreach (var prefab in prefabs)
            {
                if ((reverse && noise > prefab.level) || (!reverse && noise < prefab.level))
                {
                    if (Random.value < prefab.chance)
                    {
                        return useTexture ? GetTexturePrefab(prefab) : GetPrefab(prefab);
                    }
                }
            }
            if (returnNull) return null;
            return useTexture ? prefabs[prefabs.Length - 1].tilePrefabs[0].texture : prefabs[prefabs.Length - 1].tilePrefabs[0].prefab;
        }

        private GameObject GetPrefab(T prefab)
        {
            return GetPrefabOrTextureByWeight(prefab, false) as GameObject;
        }

        private Texture2D GetTexturePrefab(T prefab)
        {
            return GetPrefabOrTextureByWeight(prefab, true) as Texture2D;
        }

        private Object GetPrefabOrTextureByWeight(T prefab, bool useTexture)
        {
            float totalWeight = prefab.tilePrefabs.Sum(tilePrefab => tilePrefab.weight);
            float randomWeight = Random.Range(0, totalWeight);
            foreach (var tilePrefab in prefab.tilePrefabs)
            {
                if (randomWeight < tilePrefab.weight)
                {
                    return useTexture ? tilePrefab.texture : tilePrefab.prefab;
                }
                randomWeight -= tilePrefab.weight;
            }
            return null;
        }
    }
}