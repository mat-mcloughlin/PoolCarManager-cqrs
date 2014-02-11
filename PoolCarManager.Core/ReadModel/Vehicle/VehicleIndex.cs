namespace PoolCarManager.Core.ReadModel.Vehicle
{
    using System;

    using PoolCarManager.Core.Repository;

    public class VehicleIndex : EntityBase
    {
        public Guid AggregateId { get; set; }

        public string Registration { get; set; }

        public string Description { get; set; }
    }
}
