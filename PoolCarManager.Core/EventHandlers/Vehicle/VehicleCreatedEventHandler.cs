namespace PoolCarManager.Core.EventHandlers.Vehicle
{
    using PoolCarManager.Core.Events.Vehicle;
    using PoolCarManager.Core.ReadModel.Vehicle;
    using PoolCarManager.Core.Repository;

    public class VehicleCreatedEventHandler : IEventHandler<VehicleCreatedEvent>
    {
        private readonly IRepository<VehicleIndex> repository;

        public VehicleCreatedEventHandler(IRepository<VehicleIndex> repository)
        {
            this.repository = repository;
        }

        public void Execute(VehicleCreatedEvent @event)
        {
            this.repository.Insert(new VehicleIndex { AggregateId = @event.Id, Description = @event.Description, Registration = @event.Registration });
        }
    }
}