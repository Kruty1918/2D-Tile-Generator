using UnityEngine;

namespace Systems.WorldGenerator_
{
    // Інтерфейс для генерації об'єктів
    public interface IObjectGenerator<T>
    {
        GameObject GenerateObject(float noise, T[] prefabs, bool returnNull = false, bool reverse = false);
        Texture2D GenerateTextureObject(float noise, T[] prefabs, bool returnNull = false, bool reverse = false);
    }
}

