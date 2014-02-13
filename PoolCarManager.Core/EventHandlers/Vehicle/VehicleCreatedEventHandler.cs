namespace PoolCarManager.Core.EventHandlers.Vehicle
{
    using PoolCarManager.Core.Events.Vehicle;
    using PoolCarManager.Core.Projections.Vehicle;
    using PoolCarManager.Core.Repository;

    public class VehicleCreatedEventHandler : IHandler<VehicleCreatedEvent>
    {
        private readonly IRepository<VehicleIndex> vehicleIndexRepository;

        private readonly IRepository<VehicleDetails> vehicleDetailsRepository;

        public VehicleCreatedEventHandler(IRepository<VehicleIndex> vehicleIndexRepository, IRepository<VehicleDetails> vehicleDetailsRepository)
        {
            this.vehicleIndexRepository = vehicleIndexRepository;
            this.vehicleDetailsRepository = vehicleDetailsRepository;
        }

        public void Execute(VehicleCreatedEvent @event)
        {
            this.vehicleIndexRepository.Insert(new VehicleIndex { AggregateId = @event.Id, Description = @event.Description, Registration = @event.Registration });
            this.vehicleDetailsRepository.Insert(new VehicleDetails { AggregateId = @event.Id, Description = @event.Description, Registration = @event.Registration });
        }
    }
}