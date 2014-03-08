﻿namespace PoolCarManager.Core.EventStore
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