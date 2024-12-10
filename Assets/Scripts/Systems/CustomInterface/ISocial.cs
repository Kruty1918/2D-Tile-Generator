namespace Systems.CustomInterface
{
    /// <summary>
    /// Інтерфейс ISocial представляє соціальні характеристики об'єкта.
    /// </summary>
    public interface ISocial
    {
        /// <summary>
        /// Отримує або встановлює репутацію об'єкта.
        /// </summary>
        float Reputation { get; set; }

        /// <summary>
        /// Збільшує репутацію об'єкта на вказану суму.
        /// </summary>
        /// <param name="amount">Сума, на яку збільшується репутація.</param>
        void IncreaseReputation(float amount);

        /// <summary>
        /// Зменшує репутацію об'єкта на вказану суму.
        /// </summary>
        /// <param name="amount">Сума, на яку зменшується репутація.</param>
        void DecreaseReputation(float amount);
    }
}