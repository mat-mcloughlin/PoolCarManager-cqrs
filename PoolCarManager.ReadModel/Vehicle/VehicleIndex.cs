namespace PoolCarManager.ReadModel.Vehicle
{
    using System;

    using Repository;

    public class VehicleIndex : EntityBase
    {
        public Guid AggregateId { get; set; }

        public string Registration { get; set; }

        public string Description { get; set; }
    }
}
