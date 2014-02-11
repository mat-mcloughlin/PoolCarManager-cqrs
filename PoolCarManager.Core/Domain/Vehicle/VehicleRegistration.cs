namespace PoolCarManager.Core.Domain.Vehicle
{
    public class VehicleRegistration
    {
        public VehicleRegistration(string regisration)
        {
            this.Registration = regisration;
        }

        public string Registration { get; private set; }
    }
}