namespace PoolCarManager.Commands
{
    using System;

    [Serializable]
    public class Command : ICommand
    {
        public Command(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }
    }
}