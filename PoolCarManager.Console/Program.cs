﻿using System;

namespace PoolCarManager.Console
{
    using System.Reflection;

    using Autofac;

    using CommandHandlers;
    
    using Commands.Vehicle;
    using Core;
    using EventHandlers;
    using EventStore;

    using PoolCarManager.CommandHandlers.Vehicle;

    using ReadModel.Vehicle;
    using Repository;

    public class Program
    {
        public static void Main(string[] args)
        {
            //// Run MongoDB and EventStore
            //// c:\Personal\PoolCarManager\MongoDB\mongod.exe --dbpath c:\Personal\PoolCarManager\mongodb\data
            //// c:\Personal\PoolCarManager\EventStore\EventStore.SingleNode.exe --db c:\Personal\PoolCarManager\EventStore\ESData
            
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(MongoDbRepository<>)).As(typeof(IRepository<>));
            builder.RegisterAssemblyTypes(Assembly.Load("PoolCarManager.EventHandlers")).AsClosedTypesOf(typeof(IEventHandler<>));
            builder.RegisterAssemblyTypes(Assembly.Load("PoolCarManager.CommandHandlers")).AsClosedTypesOf(typeof(ICommandHandler<>));
            builder.Register(x => BusFactory.Create(new AutofacAdapter(x.Resolve<IComponentContext>())));
            builder.RegisterType<DomainRepository>().As<IDomainRepository>();
            
            var container = builder.Build();
            
            var repository = container.Resolve<IDomainRepository>();
            var guid = Guid.NewGuid();

            var createVehicleCommand = new CreateVehicleCommand(guid, "registration", "description");
            var createVehicleCommandHandler = new CreateVehicleCommandHandler(repository);
            createVehicleCommandHandler.Execute(createVehicleCommand);

            var changeVehicleDescriptionCommand = new ChangeVehicleDescriptionCommand(guid, "newDescription");
            var changeVehicleDescriptionCommandHandler = new ChangeVehicleDescriptionCommandHandler(repository);
            changeVehicleDescriptionCommandHandler.Execute(changeVehicleDescriptionCommand);
        }
    }
}





