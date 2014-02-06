namespace PoolCarManager.EventStore.Exceptions
{
    using System;

    public class AggregateNotFoundException : Exception
    {
        public readonly Guid Id;
        public readonly Type Type;

        public AggregateNotFoundException(Guid id, Type type)
            : base(string.Format("Aggregate '{0}' (type {1}) was not found.", id, type.Name))
        {
            this.Id = id;
            this.Type = type;
        }
    }
}
