﻿namespace PoolCarManager.Core.EventStore
 {
     using System;

     public interface IDomainRepository
     {
         TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IAggregate, new();

         void Save<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IAggregate, new();
     }
 }