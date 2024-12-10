namespace Systems.CustomInterface
{
    /// <summary>
    /// Інтерфейс IResearchable представляє об'єкти, які можуть бути досліджені.
    /// </summary>
    public interface IResearchable
    {
        /// <summary>
        /// Вказує, чи було досліджено об'єкт.
        /// </summary>
        bool IsResearched { get; }

        /// <summary>
        /// Починає дослідження об'єкта.
        /// </summary>
        void StartResearch();

        /// <summary>
        /// Завершує дослідження об'єкта.
        /// </summary>
        void FinishResearch();
    }
}