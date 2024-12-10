namespace Systems.CustomInterface
{
    /// <summary>
    /// Інтерфейс IObjectImpact представляє обєкти, які можуть впливати на різні аспекти гри.
    /// </summary>
    public interface IObjectImpact
    {
        /// <summary>
        /// Застосовує вплив обєкта на гру.
        /// </summary>
        void ApplyImpact();
    }
}