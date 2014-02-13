namespace PoolCarManager.Core
{
    using System;
    using System.Collections.Generic;

    using Autofac;

    using MemBus;

    public class AutofacAdapter : IocAdapter
    {
        private readonly IComponentContext container;

        public AutofacAdapter(IComponentContext container)
        {
            this.container = container;
        }

        public IEnumerable<object> GetAllInstances(Type desiredType)
        {
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(desiredType);
            var handlers = this.container.Resolve(enumerableType) as IEnumerable<object>;

            return handlers;
        }
    }
}
