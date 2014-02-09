using MemBus;
using PoolCarManager.Commands.Vehicle;
using PoolCarManager.Repository;
using PoolCarManager.ReadModel.Vehicle;
using PoolCarManager.CommandHandlers.Vehicle;

namespace PoolCarManager.Web.Modules
{
	using System;
	using Autofac;
	using Nancy;
	using Nancy.ModelBinding;

	using PoolCarManager.EventStore;
	
	public class VehicleModule : NancyModule
    {
		public VehicleModule (IBus bus, IRepository<VehicleIndex> vehicleIndexRepository, IDomainRepository domainRepository) : base("/Vehicle")
        {
			Get["/"] = _ =>
			{
				var vehicles = vehicleIndexRepository.GetAll();
				return View["Index", vehicles];
			};

			Get["/CreateVehicle"] = _ =>
			{
				return View["Create"];
			};

			Post["/CreateVehicle"] = _ =>
			{
				var command = new CreateVehicleCommand(Guid.NewGuid(), Request.Form.Registration, Request.Form.Description);
				var commandHandler = new CreateVehicleCommandHandler(domainRepository);
				commandHandler.Execute(command);
				return Response.AsRedirect("/Vehicle");
			};
        }
    }
}

