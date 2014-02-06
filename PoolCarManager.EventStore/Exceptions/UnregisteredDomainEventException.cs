namespace PoolCarManager.EventStore.Exceptions
{
    using System;

    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message)
            : base(message)
        {
        }
    }
}
