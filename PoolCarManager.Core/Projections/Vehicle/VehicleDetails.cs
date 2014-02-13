namespace PoolCarManager.Core.Projections.Vehicle
{
    using PoolCarManager.Core.Repository;

    public class VehicleDetails : EntityBase
    {
        public string Registration { get; set; }

        public string Description { get; set; }
    }
}
