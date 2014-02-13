namespace PoolCarManager.Core.CommandHandlers.Vehicle
{
    using PoolCarManager.Core.Commands.Vehicle;
    using PoolCarManager.Core.Domain.Vehicle;
    using PoolCarManager.Core.EventStore;

    public class ChangeVehicleDetailsCommandHandler : IHandler<ChangeVehicleDetailsCommand>
    {
        private readonly IDomainRepository repository;

        public ChangeVehicleDetailsCommandHandler(IDomainRepository repository)
        {
            this.repository = repository;
        }

        public void Execute(ChangeVehicleDetailsCommand command)
        {
            var vehicle = this.repository.GetById<Vehicle>(command.Id);
            vehicle.ChangeDetails(new VehicleRegistration(command.Registration), new VehicleDescription(command.Description));

            this.repository.Save(vehicle);
        }
    }
}
