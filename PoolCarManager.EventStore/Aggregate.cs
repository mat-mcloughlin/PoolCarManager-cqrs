namespace PoolCarManager.EventStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Events;
    using EventStore.Exceptions;

    public class Aggregate : IAggregate
    {
        private readonly Dictionary<Type, Action<IEvent>> registeredEvents;
        private readonly List<IEvent> appliedEvents;

        public Aggregate()
        {
            this.registeredEvents = new Dictionary<Type, Action<IEvent>>();
            this.appliedEvents = new List<IEvent>();
        }

        public Guid Id { get; protected set; }

        public int Version { get; protected set; }

        void IAggregate.LoadFromHistory(IEnumerable<IEvent> domainEvents)
        {
            if (!domainEvents.Any())
            {
                return;
            }

            foreach (var domainEvent in domainEvents)
            {
                this.Apply(domainEvent.GetType(), domainEvent);
            }

            this.Version = domainEvents.Last().Version;
        }

        IEnumerable<IEvent> IAggregate.GetChanges()
        {
            return this.appliedEvents.Where(e => e.Version >= this.Version).OrderBy(e => e.Version).ToList();
        }

        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, IEvent
        {
            this.registeredEvents.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
        }

        protected void Apply(IEvent @event)
        {
            @event.Version = this.GetNewEventVersion();
            this.Apply(@event.GetType(), @event);
            this.appliedEvents.Add(@event);
        }

        private void Apply(Type eventType, IEvent @event)
        {
            Action<IEvent> handler;

            if (!this.registeredEvents.TryGetValue(eventType, out handler))
            {
                throw new UnregisteredDomainEventException(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, this.GetType().FullName));
            }

            handler(@event);
        }

        private int GetNewEventVersion()
        {
            return ++this.Version;
        }
    }
}