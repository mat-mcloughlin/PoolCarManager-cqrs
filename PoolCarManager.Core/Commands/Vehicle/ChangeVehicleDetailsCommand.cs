namespace PoolCarManager.Core.Commands.Vehicle
{
    using System;

    public class ChangeVehicleDetailsCommand : Command
    {
        public ChangeVehicleDetailsCommand(Guid id, string registration, string description)
            : base(id)
        {
            this.Registration = registration;
            this.Description = description;
        }

        public string Registration { get; set; }

        public string Description { get; private set; }
    }
}
