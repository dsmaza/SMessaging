using System.Reflection;
using SMessaging;
using SMessaging.Abstractions;
using SMessaging.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static void AddSimpleMessaging(this IServiceCollection services, HandlerScanner handlerScanner = null)
        {
            if (handlerScanner == null)
            {
                handlerScanner = new HandlerScanner();
                var assembly = Assembly.GetCallingAssembly();
                handlerScanner.ScanAssembly(assembly);
            }

            foreach (var item in handlerScanner)
            {
                services.AddTransient(item.Value);
            }

            services.AddSingleton<IMessaging>(serviceProvider => new SimpleMessaging(new MsDiHandlerProvider(serviceProvider), handlerScanner));
        }
    }
}
