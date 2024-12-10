namespace Systems.CustomInterface
{
    /// <summary>
    /// Інтерфейс ITradable представляє об'єкти, які можуть бути куплені або продані в магазинах або на ринках.
    /// </summary>
    public interface ITradable
    {
        /// <summary>
        /// Ціна товару або послуги.
        /// </summary>
        float Price { get; set; }

        /// <summary>
        /// Купує товар або послугу.
        /// </summary>
        /// <param name="quantity">Кількість товару або послуги, яку потрібно купити.</param>
        void Buy(int quantity);

        /// <summary>
        /// Продає товар або послугу.
        /// </summary>
        /// <param name="quantity">Кількість товару або послуги, яку потрібно продати.</param>
        void Sell(int quantity);
    }
}
