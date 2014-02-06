namespace PoolCarManager.Commands.Vehicle
{
    using System;

    public class ChangeVehicleDescriptionCommand : Command
    {
        public ChangeVehicleDescriptionCommand(Guid id, string description)
            : base(id)
        {
            this.Description = description;
        }

        public string Description { get; private set; }
    }
}
