using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.WorldGenerator_
{
    [BurstCompile]
    public struct NoiseJob : IJobParallelFor
    {
        #region Параметри та поля

        /// <summary>
        /// Зміщення по осі X.
        /// </summary>
        public float offset_x;

        /// <summary>
        /// Зміщення по осі Y.
        /// </summary>
        public float offset_y;

        /// <summary>
        /// Масштаб шуму.
        /// </summary>
        public float scale;

        /// <summary>
        /// Сила спотворення шуму.
        /// </summary>
        public float distortionStrength;

        /// <summary>
        /// Сила "розриву" шуму.
        /// </summary>
        public float ridged;

        /// <summary>
        /// Фактор спотворення шуму.
        /// </summary>
        public float warpFactor;

        /// <summary>
        /// Вага комбінованого шуму.
        /// </summary>
        public float combineNoiseWeight;

        /// <summary>
        /// Постійність шуму.
        /// </summary>
        public float persistence;

        /// <summary>
        /// Амплітуда шуму.
        /// </summary>
        public float amplitude;

        /// <summary>
        /// Частота шуму.
        /// </summary>
        public float frequency;

        /// <summary>
        /// Множник для координати "плям".
        /// </summary>
        public float spotMultiplier;

        /// <summary>
        /// Поріг "плям".
        /// </summary>
        public float spotTreschold;

        /// <summary>
        /// Рівень моря.
        /// </summary>
        public float seaLevel;

        /// <summary>
        /// Рівень материка.
        /// </summary>
        public float matericLevel;

        /// <summary>
        /// Фактор згладжування.
        /// </summary>
        public float smotheFactor;

        /// <summary>
        /// Фактор материка.
        /// </summary>
        public float mFactor;

        /// <summary>
        /// Кількість октав.
        /// </summary>
        public int octaves;

        /// <summary>
        /// Лакунарність.
        /// </summary>
        public float lacunarity;

        /// <summary>
        /// Межі області, для якої генерується шум.
        /// </summary>
        public Bounds chunkBounds;

        /// <summary>
        /// Розмір шуму.
        /// </summary>
        public int noiseSize;

        /// <summary>
        /// Масив для зберігання результатів шуму.
        /// </summary>
        public NativeArray<float> noiseMap;

        #endregion


        #region Обчислення шумової карти

        /// <summary>
        /// Виконує обчислення шумової карти для кожного індексу.
        /// </summary>
        /// <param name="index">Індекс для обчислення.</param>
        public void Execute(int index)
        {
            int x = index / noiseSize;
            int y = index % noiseSize;

            // Обчислюємо координати X та Y.
            float xCoord = CalculateCoordinate(x, chunkBounds.min.x, offset_x, scale);
            float yCoord = CalculateCoordinate(y, chunkBounds.min.y, offset_y, scale);

            // Генеруємо шум Перліна.
            float p_noise = GeneratePerlinNoise(xCoord, yCoord);

            // Обробляємо "плями" на шумі.
            float sxCoord = CalculateSpotCoordinate(x, chunkBounds.min.x, offset_x, scale, spotMultiplier);
            float syCoord = CalculateSpotCoordinate(y, chunkBounds.min.y, offset_y, scale, spotMultiplier);
            p_noise = ApplySpotNoise(p_noise, sxCoord, syCoord);

            // Коригуємо рівні моря та материка.
            p_noise = AdjustNoiseLevel(p_noise);

            // Зберігаємо результат.
            noiseMap[index] = p_noise;
        }

        #endregion


        #region Допоміжні методи

        /// <summary>
        /// Обчислює координату для генерації шуму.
        /// </summary>
        private float CalculateCoordinate(int value, float minBound, float offset, float scale)
        {
            return (value + minBound + offset) / scale;
        }

        /// <summary>
        /// Генерує шум Перліна.
        /// </summary>
        private float GeneratePerlinNoise(float xCoord, float yCoord)
        {
            float p_noise = PerlinNoise(xCoord, yCoord);

            float distortedX = xCoord + p_noise * distortionStrength;
            float distortedY = yCoord + p_noise * distortionStrength;

            p_noise = PerlinNoise(distortedX, distortedY);
            p_noise = math.abs((p_noise - 0.5f) * ridged);

            float warpNoise = WarpNoise(xCoord, yCoord);
            p_noise = CombineNoise(p_noise, warpNoise);
            p_noise *= persistence;

            return p_noise;
        }

        /// <summary>
        /// Обчислює координату "плями" на шумі.
        /// </summary>
        private float CalculateSpotCoordinate(int value, float minBound, float offset, float scale, float multiplier)
        {
            return (value + minBound + offset) / (scale * multiplier);
        }

        /// <summary>
        /// Застосовує "пляму" на шум.
        /// </summary>
        private float ApplySpotNoise(float p_noise, float sxCoord, float syCoord)
        {
            float spotNoise = Mathf.PerlinNoise(sxCoord, syCoord);

            if (spotNoise > spotTreschold)
            {
                p_noise -= math.lerp(p_noise, math.abs(spotNoise - spotTreschold) / p_noise, 2f);
            }

            return p_noise;
        }

        /// <summary>
        /// Коригує рівні моря та материка на шумі.
        /// </summary>
        private float AdjustNoiseLevel(float p_noise)
        {
            if (p_noise <= seaLevel)
            {
                p_noise -= math.lerp(p_noise, (seaLevel - p_noise) / smotheFactor, 1);
            }

            if (p_noise >= matericLevel && matericLevel > seaLevel)
            {
                p_noise += math.lerp(p_noise, (p_noise - matericLevel) * mFactor, 1);
            }
            else if (matericLevel > seaLevel)
            {
                p_noise -= math.lerp(p_noise, (matericLevel - seaLevel), 1);
            }

            return p_noise;
        }

        /// <summary>
        /// Обчислює спотворення шуму.
        /// </summary>
        private float WarpNoise(float xCoord, float yCoord)
        {
            float warpX = (PerlinNoise(xCoord + offset_x, yCoord + offset_y) * 2 - 1) * warpFactor;
            float warpY = (PerlinNoise(xCoord + offset_x, yCoord + offset_y) * 2 - 1) * warpFactor;

            return PerlinNoise(warpX, warpY);
        }

        /// <summary>
        /// Комбінує два шуми.
        /// </summary>
        private float CombineNoise(float noiseA, float noiseB)
        {
            return noiseA * combineNoiseWeight + noiseB * (1 - combineNoiseWeight);
        }

        /// <summary>
        /// Генерує шум за допомогою функції Перліна.
        /// </summary>
        private float PerlinNoise(float xCoord, float yCoord)
        {
            float p_noise = 0;
            float maxPosibleHeigth = 1;

            float amplitudeTemp = amplitude;
            float frequencyTemp = frequency;

            for (int o = 0; o < octaves; o++)
            {
                p_noise += Mathf.PerlinNoise(xCoord * frequencyTemp, yCoord * frequencyTemp) * amplitudeTemp;
                maxPosibleHeigth += amplitudeTemp;
                amplitudeTemp *= frequencyTemp;
                frequencyTemp *= lacunarity;
            }

            p_noise /= maxPosibleHeigth;

            return p_noise;
        }

        #endregion
    }
}