namespace PoolCarManager.Web.Modules
{
    using System;

    using MemBus;

    using Nancy;

    using PoolCarManager.Core.Commands.Vehicle;
    using PoolCarManager.Core.Projections.Vehicle;
    using PoolCarManager.Core.Repository;

    public class VehicleModule : NancyModule
    {
        public VehicleModule(IBus bus, IRepository<VehicleIndex> vehicleIndexRepository, IRepository<VehicleDetails> vehicleDetailsRepository)
            : base("/Vehicle")
        {
            this.Get["/"] = _ =>
            {
                var vehicles = vehicleIndexRepository.GetAll();
                return View["Index", vehicles];
            };

            this.Get["/{id:guid}"] = args =>
            {
                var vehicle = vehicleDetailsRepository.GetByAggregateId(args.id);
                return View["Details", vehicle];
            };

            this.Get["/Create"] = _ =>
            {
                return View["Create"];
            };

            this.Post["/Create"] = _ =>
            {
                var command = new CreateVehicleCommand(Guid.NewGuid(), Request.Form.Registration, Request.Form.Description);
                bus.Publish(command);
                return Response.AsRedirect("/Vehicle");
            };
        }
    }
}

