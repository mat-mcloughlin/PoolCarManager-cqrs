namespace PoolCarManager.Core.Domain.Vehicle
{
    using System;

    public class NonExistingVehicleException : Exception
    {
        public NonExistingVehicleException(string message)
            : base(message)
        {
        }
    }
}