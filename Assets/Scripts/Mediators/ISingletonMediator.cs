namespace Mediators.Singleton
{
    public interface ISingletonMediator<T> where T : class
    {
        T Instance { get; }
    }
}