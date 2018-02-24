using System;
using System.Threading.Tasks;
using SMessaging.Abstractions;
using SMessaging.Internal;

namespace SMessaging
{
    public sealed class SimpleMessaging : IMessaging
    {
        private readonly IMessaging messagingCore;

        public SimpleMessaging()
            : this(new HandlerProvider(null), CreateHandlerScanner(System.Reflection.Assembly.GetCallingAssembly()))
        {
        }

        public SimpleMessaging(HandlerScanner handlerScanner, IServiceProvider serviceProvider = null)
            : this(new HandlerProvider(serviceProvider), handlerScanner)
        {
        }

        public SimpleMessaging(IHandlerProvider handlerProvider, HandlerScanner handlerScanner)
        {
            messagingCore = new MessagingCore(handlerProvider, handlerScanner);
        }

        public Task<MessageResult> Send<TMessage>(TMessage message)
        {
            return messagingCore.Send(message);
        }

        private static HandlerScanner CreateHandlerScanner(System.Reflection.Assembly assembly)
        {
            var handlerScanner = new HandlerScanner();
            handlerScanner.ScanAssembly(assembly);
            return handlerScanner;
        }
    }
}
