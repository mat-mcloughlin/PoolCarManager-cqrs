namespace PoolCarManager.Commands.Vehicle
{
    using System;

    public class CreateVehicleCommand : Command
    {
        public CreateVehicleCommand(Guid id, string registration, string description)
            : base(id)
        {
            this.Registration = registration;
            this.Description = description;
        }

        public string Registration { get; private set; }

        public string Description { get; private set; }
    }
}
