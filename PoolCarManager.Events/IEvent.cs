namespace PoolCarManager.Events
{
    using System;

    public interface IEvent
    {
        Guid Id { get; }

        Guid AggregateId { get; set; }

        int Version { get; set; }
    }
}
