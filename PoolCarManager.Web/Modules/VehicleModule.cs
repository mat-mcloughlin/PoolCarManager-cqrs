namespace PoolCarManager.Web.Modules
{
    using System;

    using MemBus;

    using Nancy;

    using PoolCarManager.Core.CommandHandlers.Vehicle;
    using PoolCarManager.Core.Commands.Vehicle;
    using PoolCarManager.Core.EventStore;
    using PoolCarManager.Core.ReadModel.Vehicle;
    using PoolCarManager.Core.Repository;

    public class VehicleModule : NancyModule
    {
        public VehicleModule(IBus bus, IRepository<VehicleIndex> vehicleIndexRepository, IDomainRepository domainRepository)
            : base("/Vehicle")
        {
            this.Get["/"] = _ =>
            {
                var vehicles = vehicleIndexRepository.GetAll();
                return View["Index", vehicles];
            };

            this.Get["/CreateVehicle"] = _ =>
            {
                return View["Create"];
            };

            this.Post["/CreateVehicle"] = _ =>
            {
                var command = new CreateVehicleCommand(Guid.NewGuid(), Request.Form.Registration, Request.Form.Description);
                var commandHandler = new CreateVehicleCommandHandler(domainRepository);
                commandHandler.Execute(command);
                return Response.AsRedirect("/Vehicle");
            };
        }
    }
}

