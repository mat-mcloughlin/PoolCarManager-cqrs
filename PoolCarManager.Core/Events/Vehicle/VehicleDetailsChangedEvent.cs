namespace PoolCarManager.Core.Events.Vehicle
{
    using System;

    [Serializable]
    public class VehicleDetailsChangedEvent : Event
    {
        public VehicleDetailsChangedEvent(Guid vehicleId, string registration, string description)
        {
            this.AggregateId = vehicleId;
            this.Registration = registration;
            this.Description = description;
        }

        public string Registration { get; private set; }

        public string Description { get; private set; }
    }
}