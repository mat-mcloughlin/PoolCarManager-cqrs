﻿namespace PoolCarManager.Core.Repository
{
    using System;

    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        public Guid AggregateId { get; set; }
    }
}
