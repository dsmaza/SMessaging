using System;
using SMessaging.Abstractions;

namespace SMessaging.Internal
{
    class HandlerProvider : IHandlerProvider
    {
        private readonly IServiceProvider serviceProvider;

        public HandlerProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? new ActivatorServiceProvider();
        }

        public IHandlerScope CreateScope()
        {
            return new NullHandlerScope(t => serviceProvider.GetService(t));
        }

        private class ActivatorServiceProvider : IServiceProvider
        {
            public object GetService(Type serviceType)
            {
                return Activator.CreateInstance(serviceType);
            }
        }

        private class NullHandlerScope : IHandlerScope
        {
            private Func<Type, object> serviceProvider;

            public NullHandlerScope(Func<Type, object> serviceProvider)
            {
                this.serviceProvider = serviceProvider;
            }

            public object GetService(Type serviceType)
            {
                return serviceProvider?.Invoke(serviceType);
            }

            public void Dispose()
            {
                serviceProvider = null;
            }
        }
    }
}
