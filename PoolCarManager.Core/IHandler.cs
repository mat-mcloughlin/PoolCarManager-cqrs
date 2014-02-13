namespace PoolCarManager.Core
{
    public interface IHandler<in T> where T : class
    {
        void Execute(T command);
    }
}
