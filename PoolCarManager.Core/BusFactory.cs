namespace PoolCarManager.Core
{
    using EventHandlers;

    using MemBus;
    using MemBus.Configurators;
    
    public static class BusFactory
    {
        public static IBus Create(IocAdapter iocAdapter)
        {
            return BusSetup.StartWith<Conservative>()
                .Apply<IoCSupport>(s => s.SetAdapter(iocAdapter).SetHandlerInterface(typeof(IEventHandler<>)))
                .Construct();
        }
    }
}
