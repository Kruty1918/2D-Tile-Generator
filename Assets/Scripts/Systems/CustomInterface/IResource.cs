namespace Systems.CustomInterface
{
    /// <summary>
    /// Інтерфейс IResource представляє ресурси, які можуть бути зібрані або використані.
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// Кількість ресурсів, які залишилися.
        /// </summary>
        float ResourceQuantity { get; set; }

        /// <summary>
        /// Додає вказану кількість ресурсів.
        /// </summary>
        /// <param name="amount">Кількість ресурсів для додавання.</param>
        void Add(float amount);

        /// <summary>
        /// Видаляє вказану кількість ресурсів.
        /// </summary>
        /// <param name="amount">Кількість ресурсів для видалення.</param>
        void Remove(float amount);
    }
}