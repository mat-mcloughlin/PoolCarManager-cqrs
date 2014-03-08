﻿namespace PoolCarManager.Core.EventStore
 {
     using System;
﻿using System.Collections.Generic;
﻿using PoolCarManager.Core.Events;

     public interface IAggregate
     {
         Guid Id { get; }

         int Version { get; }

         void LoadFromHistory(IEnumerable<IEvent> domainEvents);

         IEnumerable<IEvent> GetChanges();

     }
 }