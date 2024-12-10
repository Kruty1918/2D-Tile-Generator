using System;
using UnityEngine;

namespace Systems.WorldGenerator_
{
    /// <summary>
    /// Клас, що містить налаштування для генерації карт шуму.
    /// </summary>
    [Serializable]
    public class NoiseMapSettings
    {
        /// <summary>
        /// Масштаб карти шуму. Більший масштаб призведе до більших особливостей.
        /// </summary>
        [Header("Налаштування масштабу")]
        [Tooltip("Масштаб карти шуму.")]
        [Range(0.1f, 1000.0f)]
        public float scale = 20.0f;

        /// <summary>
        /// Множник для розміру плями. Більше значення призведе до більших плям.
        /// </summary>
        [Header("Налаштування плям")]
        [Tooltip("Множник для розміру плями.")]
        [Range(1f, 150.0f)]
        public float spotMultiplier = 1;

        /// <summary>
        /// Поріг для плям. Враховуються лише плями зі значенням шуму вище цього порогу.
        /// </summary>
        [Tooltip("Поріг для плям.")]
        [Range(0, 1)]
        public float spotTreschold = 0.5f;

        /// <summary>
        /// Кількість октав для використання для шуму Перліна. Більше октав призведе до більшої деталізації.
        /// </summary>
        [Header("Налаштування октав")]
        [Tooltip("Кількість октав для шуму Перліна.")]
        [Range(1, 12)]
        public byte octaves = 4;

        /// <summary>
        /// Контролює зменшення амплітуди кожної октави.
        /// </summary>
        [Header("Налаштування постійності")]
        [Tooltip("Контролює зменшення амплітуди кожної октави.")]
        [Range(1f, 10f)]
        public float persistence = 1;

        /// <summary>
        /// Контролює збільшення частоти кожної октави.
        /// </summary>
        [Header("Налаштування лакунарності")]
        [Tooltip("Контролює збільшення частоти кожної октави.")]
        [Range(1.0f, 5.0f)]
        public float lacunarity = 2.0f;

        /// <summary>
        /// Частота першої октави.
        /// </summary>
        [Header("Налаштування частоти")]
        [Tooltip("Частота першої октави.")]
        [Range(0.0f, 2.0f)]
        public float frequency = 0.5f;

        /// <summary>
        /// Амплітуда для усіх октав.
        /// </summary>
        [Header("Налаштування амплітуди")]
        [Tooltip("Амплітуда для усіх октав.")]
        [Range(0.0f, 5.0f)]
        public float amplitude = 1.0f;

        /// <summary>
        /// Контролює гостроту гребенів у шумі.
        /// </summary>
        [Header("Налаштування гостроти")]
        [Tooltip("Контролює гостроту гребенів у шумі.")]
        [Range(0, 7)]
        public float ridged = 1;

        /// <summary>
        /// Рівень моря на карті шуму. Значення нижче цього будуть вважатися морем.
        /// </summary>
        [Header("Налаштування рівня моря")]
        [Tooltip("Рівень моря на карті шуму.")]
        [Range(-1, 1)]
        public float seaLevel = 0;

        /// <summary>
        /// Рівень материка на карті шуму. Значення вище цього будуть вважатися сушею.
        /// </summary>
        [Header("Налаштування рівня материка")]
        [Tooltip("Рівень материка на карті шуму.")]
        [Range(-1, 1)]
        public float matericLevel = 0;

        /// <summary>
        /// Контролює плавність переходу між морем та сушею.
        /// </summary>
        [Header("Налаштування фактора згладжування")]
        [Tooltip("Контролює плавність переходу між морем та сушею.")]
        [Range(0.0f, 7.0f)]
        public float smotheFactor = 1;

        /// <summary>
        /// Контролює силу спотворення картини шуму.
        /// </summary>
        [Header("Налаштування сили спотворення")]
        [Tooltip("Контролює силу спотворення картини шуму.")]
        [Range(-10, 10)]
        public float distortionStrength = 0.0f;

        /// <summary>
        /// Вага, яка визначає, яка частина комбінованого шуму буде використовуватися.
        /// </summary>
        [Header("Налаштування ваги комбінованого шуму")]
        [Tooltip("Вага, яка визначає, яка частина комбінованого шуму буде використовуватися.")]
        [Range(0, 1)]
        public float combineNoiseWeight = 0.5f;

        /// <summary>
        /// Коефіцієнт згортання, який використовується для створення вигнутих ефектів у шумі.
        /// </summary>
        [Header("Налаштування коефіцієнта згортання")]
        [Tooltip("Коефіцієнт згортання, який використовується для створення вигнутих ефектів у шумі.")]
        [Range(0.1f, 25)]
        public float warpFactor = 0.1f;

        /// <summary>
        /// Фактор материку. Контролює, наскільки сильно впливає материк на карти шуму.
        /// </summary>
        [HideInInspector]
        public float mFactor;

        [Space(10)]

        public int seed = 1991;

        public Vector2 offset = new Vector2(1991, 1991);
    }
}