namespace Systems.CustomInterface
{
    /// <summary>
    /// Інтерфейс IWorker представляє об'єкти, які можуть збирати ресурси та будувати будівлі.
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        /// Збирає ресурси з вказаного джерела ресурсів.
        /// </summary>
        /// <param name="resource">Джерело ресурсів для збору.</param>
        void GatherResources(IResource resource);
    }
}