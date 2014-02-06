namespace PoolCarManager.CommandHandlers.Vehicle
{
    using Commands.Vehicle;
    using Domain.Vehicle;
    using EventStore;

    public class ChangeVehicleDescriptionCommandHandler : ICommandHandler<ChangeVehicleDescriptionCommand>
    {
        private readonly IDomainRepository repository;

        public ChangeVehicleDescriptionCommandHandler(IDomainRepository repository)
        {
            this.repository = repository;
        }

        public void Execute(ChangeVehicleDescriptionCommand command)
        {
            var vehicle = this.repository.GetById<Vehicle>(command.Id);
            vehicle.ChangeDescription(new VehicleDescription(command.Description));

            this.repository.Save(vehicle);
        }
    }
}
