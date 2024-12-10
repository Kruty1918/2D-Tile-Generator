using Systems.WorldGenerator_;
using UnityEngine;
using UnityEngine.UI;

public class NoiseTextureGenerator : MonoBehaviour
{
    public Image image; // The image to apply the noise to

    [SerializeField] private int worldSize;

    [SerializeField] private NoiseMapSettings noiseSettings;

    [SerializeField] private int minimumValidNoiseCount = 10;
    [SerializeField] private int maximumIterations = 3;
    [SerializeField] private float minimumValidNoiseValue = 0.5f;

    [SerializeField] private bool upd;

    private INoiseMapGenerator noiseMapGenerator;
    private float[,] noise;
    private NoiseMapSettings lastNoiseMapSettings;
    private int lasstWorldSize;

    private void Awake()
    {
        lastNoiseMapSettings = noiseSettings;
        lasstWorldSize = worldSize;

        noiseMapGenerator = new NoiseMap();
        GenerateValidNoiseMap();

        noise = noiseMapGenerator.GenerateMap(noiseSettings, new Bounds(Vector3.zero, Vector3.one), worldSize);
        ApplyNoise(noise);

        lasstWorldSize = worldSize;
        lastNoiseMapSettings = noiseSettings;
    }

    private void LateUpdate()
    {
        if (upd)
        {
            noise = noiseMapGenerator.GenerateMap(noiseSettings, new Bounds(Vector3.zero, Vector3.one), worldSize);
            ApplyNoise(noise);

            lasstWorldSize = worldSize;
            lastNoiseMapSettings = noiseSettings;
        }
    }

    private void GenerateValidNoiseMap()
    {
        bool validNoiseGenerated = false;
        int iterationCount = 0;

        while (!validNoiseGenerated)
        {
            noise = noiseMapGenerator.GenerateMap(noiseSettings, new Bounds(Vector3.zero, Vector3.one), worldSize);
            int validNoiseCount = CountValidNoise(noise);
            if (validNoiseCount >= minimumValidNoiseCount)
            {
                validNoiseGenerated = true;
                ApplyNoise(noise);
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
                noiseSettings.offset += new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
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

    public void ApplyNoise(float[,] noise)
    {
        if (image != null && noise != null)
        {
            Texture2D noiseTexture = GenerateNoiseTexture(noise);
            Sprite noiseSprite = Sprite.Create(noiseTexture, new Rect(0, 0, noiseTexture.width, noiseTexture.height), Vector2.one * 0.5f);
            image.sprite = noiseSprite;
        }
        else
        {
            Debug.LogError("Image or noise array is not assigned!");
        }
    }

    Texture2D GenerateNoiseTexture(float[,] noise)
    {
        int width = noise.GetLength(0);
        int height = noise.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float sample = noise[x, y];
                Color color = new Color(sample, sample, sample);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

}