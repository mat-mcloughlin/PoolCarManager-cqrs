namespace PoolCarManager.Core.Domain.Vehicle
{
    using System;
    using System.Collections.Generic;

    using PoolCarManager.Core.Events.Vehicle;
    using PoolCarManager.Core.EventStore;

    public class Vehicle : Aggregate
    {
        private readonly List<Guid> journeys;

        private string registration;

        private string description;

        public Vehicle()
        {
            this.journeys = new List<Guid>();
            this.Version = -1;
            this.RegisterEvents();
        }

        private Vehicle(Guid id, VehicleRegistration vehicleRegistration, VehicleDescription vehicleDescription)
            : this()
        {
            this.Apply(new VehicleCreatedEvent(id, vehicleRegistration.Registration, vehicleDescription.Description));
        }

        public static Vehicle CreateNew(Guid id, VehicleRegistration vehicleRegistration, VehicleDescription vehicleDescription)
        {
            return new Vehicle(id, vehicleRegistration, vehicleDescription);
        }

        public void ChangeDetails(VehicleRegistration vehicleRegistration, VehicleDescription vehicleDescription)
        {
            this.IsVehicleCreated();

            this.Apply(new VehicleDetailsChangedEvent(this.Id, vehicleRegistration.Registration, vehicleDescription.Description));
        }
        
        private void RegisterEvents()
        {
            this.RegisterEvent<VehicleCreatedEvent>(this.OnNewVehicleCreated);
            this.RegisterEvent<VehicleDetailsChangedEvent>(this.OnVehicleUpdated);
        }

        private void OnNewVehicleCreated(VehicleCreatedEvent vehicleCreatedEvent)
        {
            this.Id = vehicleCreatedEvent.AggregateId;
            this.registration = vehicleCreatedEvent.Registration;
            this.description = vehicleCreatedEvent.Description;
        }

        private void OnVehicleUpdated(VehicleDetailsChangedEvent vehicleDetailsChangedEvent)
        {
            this.description = vehicleDetailsChangedEvent.Description;
        }

        private void IsVehicleCreated()
        {
            if (this.Id == Guid.Empty)
            {
                throw new NonExistingVehicleException("The Vehicle is not created");
            }
        }
    }
}
