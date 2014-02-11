namespace PoolCarManager.Core.Events.Vehicle
{
    using System;

    [Serializable]
    public class ChangeVehicleDescriptionEvent : Event
    {
        public ChangeVehicleDescriptionEvent(string description)
        {
            this.Description = description;
        }

        public string Description { get; private set; }
    }
}