namespace Systems.CustomInterface
{
    /// <summary>
    /// Інтерфейс IBuildable представляє об'єкти, які можуть бути побудовані.
    /// </summary>
    public interface IBuildable
    {
        /// <summary>
        /// Вказує, чи була побудована будівля.
        /// </summary>
        bool IsBuilt { get; }

        /// <summary>
        /// Починає процес будівництва.
        /// </summary>
        void StartBuilding();

        /// <summary>
        /// Завершує процес будівництва.
        /// </summary>
        void FinishBuilding();
    }
}