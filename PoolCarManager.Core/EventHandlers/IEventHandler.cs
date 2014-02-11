namespace PoolCarManager.Core.EventHandlers
{
    public interface IEventHandler<in TEvent>
    {
        void Execute(TEvent @event);
    }
}   