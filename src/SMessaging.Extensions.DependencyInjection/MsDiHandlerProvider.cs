using System;
using Microsoft.Extensions.DependencyInjection;
using SMessaging.Abstractions;

namespace SMessaging.Extensions.DependencyInjection
{
    class MsDiHandlerProvider : IHandlerProvider
    {
        private readonly IServiceProvider serviceProvider;

        public MsDiHandlerProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IHandlerScope CreateScope()
        {
            return new MsDiHandlerScope(serviceProvider.CreateScope());
        }

        private class MsDiHandlerScope : IHandlerScope
        {
            private IServiceScope serviceScope;

            public MsDiHandlerScope(IServiceScope serviceScope)
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
}
