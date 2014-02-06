namespace PoolCarManager.CommandHandlers.Vehicle
{
    using Commands.Vehicle;
    using Domain.Vehicle;
    using EventStore;

    public class CreateVehicleCommandHandler : ICommandHandler<CreateVehicleCommand>
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
