namespace PoolCarManager.Core.CommandHandlers.Vehicle
{
    using PoolCarManager.Core.Commands.Vehicle;
    using PoolCarManager.Core.Domain.Vehicle;
    using PoolCarManager.Core.EventStore;

    public class CreateVehicleCommandHandler : IHandler<CreateVehicleCommand>
    {
        private readonly IDomainRepository repository;

        public CreateVehicleCommandHandler(IDomainRepository repository)
        {
            this.repository = repository;
        }

        public void Execute(CreateVehicleCommand command)
        {
            var vehicle = Vehicle.CreateNew(command.Id, new VehicleRegistration(command.Registration), new VehicleDescription(command.Description));

            this.repository.Save(vehicle);
        }
    }
}
