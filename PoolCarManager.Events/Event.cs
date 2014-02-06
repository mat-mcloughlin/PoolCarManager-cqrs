namespace PoolCarManager.Events
{
    using System;

    [Serializable]
    public class Event : IEvent
    {
        public Event()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public Guid AggregateId { get; set; }

        int IEvent.Version { get; set; }
    }
}
