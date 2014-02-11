namespace PoolCarManager.Core.Commands
{
    using System;

    public interface ICommand
    {
        Guid Id { get; }
    }
}
