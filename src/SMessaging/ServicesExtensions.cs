using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SMessaging.Abstractions;
using SMessaging.Internal;

namespace SMessaging
{
    public static class ServicesExtensions
    {
        public static void AddMessaging(this IServiceCollection services, HandlerScanner handlerScanner = null)
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

            services.AddSingleton<IMessaging>(serviceProvider => new MessagingCore(serviceProvider, handlerScanner));
        }
    }
}
