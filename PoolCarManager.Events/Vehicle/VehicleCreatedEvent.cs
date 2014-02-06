namespace PoolCarManager.Events.Vehicle
{
    using System;

    [Serializable]
    public class VehicleCreatedEvent : Event
    {
        public VehicleCreatedEvent(Guid vehicleId, string registration, string description)
        {
            this.AggregateId = vehicleId;
            this.Registration = registration;
            this.Description = description;
        }

        public string Registration { get; private set; }
        
        public string Description { get; private set; }
    }
}
