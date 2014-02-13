namespace PoolCarManager.Web.Modules
{
    using System;

    using MemBus;

    using Nancy;

    using PoolCarManager.Core.Commands.Vehicle;
    using PoolCarManager.Core.ReadModel.Vehicle;
    using PoolCarManager.Core.Repository;

    public class VehicleModule : NancyModule
    {
        public VehicleModule(IBus bus, IRepository<VehicleIndex> vehicleIndexRepository)
            : base("/Vehicle")
        {
            this.Get["/"] = _ =>
            {
                var vehicles = vehicleIndexRepository.GetAll();
                return View["Index", vehicles];
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

