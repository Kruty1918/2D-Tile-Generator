namespace Systems.CustomInterface
{
    /// <summary>
    /// Інтерфейс ICombatant представляє об'єкти, які можуть атакувати інші об'єкти.
    /// Це можуть бути воїни, монстри, герої або будь-які інші об'єкти в грі, які можуть вести бойові дії.
    /// </summary>
    public interface ICombatant
    {
        /// <summary>
        /// Метод Attack виконує атаку на цільовий об'єкт.
        /// Цільовий об'єкт повинен реалізувати інтерфейс IHealth, щоб міг отримувати пошкодження від атаки.
        /// </summary>
        /// <param name="target">Цільовий об'єкт, який буде атакований.</param>
        void Attack(IHealth target);
    }
}