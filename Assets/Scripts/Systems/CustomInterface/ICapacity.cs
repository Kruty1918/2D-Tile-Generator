namespace Systems.CustomInterface
{
    /// <summary>
    /// Інтерфейс ICapacity представляє об'єкти, які можуть містити певну кількість людей.
    /// </summary>
    public interface ICapacity
    {
        /// <summary>
        /// Максимальна кількість людей, які можуть бути в об'єкті.
        /// </summary>
        int MaxOccupancy { get; }

        /// <summary>
        /// Поточна кількість людей в об'єкті.
        /// </summary>
        int CurrentOccupancy { get; }
    }
}
