using System;
using Microsoft.Extensions.DependencyInjection;

namespace SMessaging.Internal
{
    public interface IHandlerScope : IDisposable
    {
        object GetService(Type serviceType);
    }

    class NullHandlerScope : IHandlerScope
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

    class ServiceHandlerScope : IHandlerScope
    {
        private IServiceScope serviceScope;

        public ServiceHandlerScope(IServiceScope serviceScope)
        {
            this.serviceScope = serviceScope;
        }

        public void Dispose()
        {
            serviceScope?.Dispose();
            serviceScope = null;
        }

        public object GetService(Type serviceType)
        {
            return serviceScope?.ServiceProvider?.GetService(serviceType);
        }
    }
}
