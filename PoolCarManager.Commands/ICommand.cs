namespace PoolCarManager.Commands
{
    using System;

    public interface ICommand
    {
        Guid Id { get; }
    }
}
