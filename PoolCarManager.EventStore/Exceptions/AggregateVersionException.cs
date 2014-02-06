namespace PoolCarManager.EventStore.Exceptions
{
    using System;

    public class AggregateVersionException : Exception
    {
        public readonly Guid Id;
        public readonly Type Type;
        public readonly int AggregateVersion;
        public readonly int RequestedVersion;

        public AggregateVersionException(Guid id, Type type, int aggregateVersion, int requestedVersion)
            : base(string.Format("Requested version {2} of aggregate '{0}' (type {1}) - aggregate version is {3}", id, type.Name, requestedVersion, aggregateVersion))
        {
           this.Id = id;
           this.Type = type;
           this.AggregateVersion = aggregateVersion;
           this.RequestedVersion = requestedVersion;
        }
    }
}
