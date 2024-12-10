namespace Systems.CustomInterface
{
    /// <summary>
    /// Інтерфейс IEconomy представляє вплив об'єкта на загальну економіку.
    /// </summary>
    public interface IEconomy
    {
        /// <summary>
        /// Отримує або встановлює економічний вплив об'єкта.
        /// </summary>
        float EconomicImpact { get; set; }

        /// <summary>
        /// Збільшує економічний вплив об'єкта на вказану суму.
        /// </summary>
        /// <param name="amount">Сума, на яку збільшується економічний вплив.</param>
        void IncreaseEconomicImpact(float amount);

        /// <summary>
        /// Зменшує економічний вплив об'єкта на вказану суму.
        /// </summary>
        /// <param name="amount">Сума, на яку зменшується економічний вплив.</param>
        void DecreaseEconomicImpact(float amount);
    }
}
