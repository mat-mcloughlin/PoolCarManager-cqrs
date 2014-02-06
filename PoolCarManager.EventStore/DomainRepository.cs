namespace PoolCarManager.EventStore
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Text;

    using global::EventStore.ClientAPI;

    using MemBus;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using PoolCarManager.Events;
    using PoolCarManager.EventStore.Exceptions;

    public class DomainRepository : IDomainRepository
    {
        private const string EventClrTypeHeader = "EventClrTypeName";
        private const string AggregateClrTypeHeader = "AggregateClrTypeName";
        private const string CommitIdHeader = "CommitId";
        private const int WritePageSize = 500;
        private const int ReadPageSize = 500;

        private readonly IBus bus;
        private readonly IEventStoreConnection eventStoreConnection;

        public DomainRepository(IBus bus)
        {
            this.bus = bus;
            this.eventStoreConnection = SetupConnection();
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IAggregate, new()
        {
            return this.GetById<TAggregate>(id, int.MaxValue);
        }

        public TAggregate GetById<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate
        {
            if (version <= 0)
            {
                throw new InvalidOperationException("Cannot get version <= 0");
            }

            var streamName = AggregateIdToStreamName(typeof(TAggregate), id);
            var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), true);

            var sliceStart = 0;
            StreamEventsSlice currentSlice;
           
            do
            {
                var sliceCount = sliceStart + ReadPageSize <= version ? ReadPageSize : version - sliceStart + 1;

                currentSlice = this.eventStoreConnection.ReadStreamEventsForward(streamName, sliceStart, sliceCount, false);

                if (currentSlice.Status == SliceReadStatus.StreamNotFound)
                {
                    throw new AggregateNotFoundException(id, typeof(TAggregate));
                }

                if (currentSlice.Status == SliceReadStatus.StreamDeleted)
                {
                    throw new AggregateDeletedException(id, typeof(TAggregate));
                }

                sliceStart = currentSlice.NextEventNumber;

                var deserializedEvents = currentSlice.Events.Select(e => DeserializeEvent(e.OriginalEvent.Metadata, e.OriginalEvent.Data));

                aggregate.LoadFromHistory(deserializedEvents);
            }
            while (version >= currentSlice.NextEventNumber && !currentSlice.IsEndOfStream);

            if (aggregate.Version != version && version < int.MaxValue)
            {
                throw new AggregateVersionException(id, typeof(TAggregate), aggregate.Version, version);
            }

            return aggregate;
        }

        public void Save<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IAggregate, new()
        {
            var commitHeaders = new Dictionary<string, object>
            {
                { CommitIdHeader, Guid.NewGuid() },
                { AggregateClrTypeHeader, aggregateRoot.GetType().AssemblyQualifiedName }
            };

            var streamName = AggregateIdToStreamName(typeof(TAggregate), aggregateRoot.Id);
            var newEvents = aggregateRoot.GetChanges();
            var expectedVersion = aggregateRoot.Version - newEvents.Count();
            var eventsToSave = newEvents.Select(e => ToEventData(Guid.NewGuid(), e, commitHeaders)).ToList();

            if (eventsToSave.Count < WritePageSize)
            {
                this.eventStoreConnection.AppendToStream(streamName, expectedVersion, eventsToSave);
            }
            else
            {
                var transaction = this.eventStoreConnection.StartTransaction(streamName, expectedVersion);

                var position = 0;
                while (position < eventsToSave.Count)
                {
                    var pageEvents = eventsToSave.Skip(position).Take(WritePageSize);
                    transaction.Write(pageEvents);
                    position += WritePageSize;
                }

                transaction.Commit();
            }

            foreach (var domainEvent in newEvents)
            {
                this.bus.Publish(domainEvent);
            }
        }

        private static EventData ToEventData(Guid eventId, object evnt, IDictionary<string, object> headers)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evnt));

            var eventHeaders = new Dictionary<string, object>(headers)
            {
                {
                    EventClrTypeHeader, evnt.GetType().AssemblyQualifiedName
                }
            };
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders));
            var typeName = evnt.GetType().Name;

            return new EventData(eventId, typeName, true, data, metadata);
        }

        private static IEvent DeserializeEvent(byte[] metadata, byte[] data)
        {
            var eventClrTypeName = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(EventClrTypeHeader).Value;
            return (IEvent)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string)eventClrTypeName));
        }

        private static string AggregateIdToStreamName(Type type, Guid guid)
        {
            return string.Format("{0}-{1}", char.ToLower(type.Name[0]) + type.Name.Substring(1), guid.ToString("N"));
        }

        private static IEventStoreConnection SetupConnection()
        {
            var settings = ConnectionSettings.Default;
            var ip = IPAddress.Parse(ConfigurationManager.AppSettings.Get("EventStoreIPAddress"));
            var tcpPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("EventStorePort"));
            var tcpEndPoint = new IPEndPoint(ip, tcpPort);
            var eventStoreConnection = EventStoreConnection.Create(settings, tcpEndPoint);
            eventStoreConnection.Connect();
            return eventStoreConnection;
        }
    }
}
