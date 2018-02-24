using System;
using Microsoft.Extensions.DependencyInjection;

namespace SMessaging.Internal
{
    class HandlerProvider
    {
        private readonly IServiceProvider serviceProvider;
        private readonly bool useMSDependencyInjection;

        public HandlerProvider(IServiceProvider serviceProvider)
        {
            useMSDependencyInjection = serviceProvider?.GetType().Namespace == "Microsoft.Extensions.DependencyInjection";
            this.serviceProvider = serviceProvider ?? new ActivatorServiceProvider();
        }

        public IHandlerScope CreateScope()
        {
            if (useMSDependencyInjection)
            {
                return new ServiceHandlerScope(serviceProvider.CreateScope());
            }
            return new NullHandlerScope(t => serviceProvider.GetService(t));
        }

        private class ActivatorServiceProvider : IServiceProvider
        {
            public object GetService(Type serviceType)
            {
                return Activator.CreateInstance(serviceType);
            }
        }
    }
}
