namespace PoolCarManager.Core.Domain.Vehicle
{
    public class VehicleDescription
    {
        public VehicleDescription(string description)
        {
            this.Description = description;
        }

        public string Description { get; private set; }
    }
}