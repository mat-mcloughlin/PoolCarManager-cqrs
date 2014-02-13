namespace PoolCarManager.Web
{
    using System.Reflection;

    using Autofac;

    using Nancy;
    using Nancy.Bootstrappers.Autofac;

    using PoolCarManager.Core;
    using PoolCarManager.Core.EventStore;
    using PoolCarManager.Core.Repository;

    public class Bootstrapper : AutofacNancyBootstrapper
    {
        public Bootstrapper()
        {
            StaticConfiguration.DisableErrorTraces = false;
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            var builder = new ContainerBuilder();

            // Perform registration that should have an application lifetime
            builder.RegisterGeneric(typeof(MongoDbRepository<>)).As(typeof(IRepository<>));
            builder.RegisterAssemblyTypes(Assembly.Load("PoolCarManager.Core")).AsClosedTypesOf(typeof(IHandler<>));
            builder.Register(x => BusFactory.Create(new AutofacAdapter(x.Resolve<IComponentContext>())));
            builder.RegisterType<DomainRepository>().As<IDomainRepository>();
            builder.Update(existingContainer.ComponentRegistry);
        }
    }
}

