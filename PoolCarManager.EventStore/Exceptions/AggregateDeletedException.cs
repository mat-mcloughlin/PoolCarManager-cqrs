namespace PoolCarManager.EventStore.Exceptions
{
    using System;

    public class AggregateDeletedException : Exception
    {
        public readonly Guid Id;
        public readonly Type Type;

        public AggregateDeletedException(Guid id, Type type)
            : base(string.Format("Aggregate '{0}' (type {1}) was deleted.", id, type.Name))
        {
            this.Id = id;
            this.Type = type;
        }
    }
}
