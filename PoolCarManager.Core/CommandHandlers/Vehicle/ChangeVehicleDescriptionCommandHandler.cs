namespace PoolCarManager.Core.CommandHandlers.Vehicle
{
    using PoolCarManager.Core.Commands.Vehicle;
    using PoolCarManager.Core.Domain.Vehicle;
    using PoolCarManager.Core.EventStore;

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
