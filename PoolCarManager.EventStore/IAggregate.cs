namespace PoolCarManager.EventStore
{
    using System;
    using System.Collections.Generic;

    using Events;

    public interface IAggregate
    {
        Guid Id { get; }

        int Version { get; }

        void LoadFromHistory(IEnumerable<IEvent> domainEvents);

        IEnumerable<IEvent> GetChanges();

    }
}
