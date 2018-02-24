using System;
using SMessaging.Abstractions;
using SMessaging.Internal;

namespace SMessaging
{
    public sealed class MessagingProvider
    {
        private HandlerProvider serviceProvider;
        private HandlerScanner handlerScanner;
        private IMessaging messaging;

        public MessagingProvider(IServiceProvider serviceProvider = null, HandlerScanner handlerScanner = null)
        {
            this.serviceProvider = new HandlerProvider(serviceProvider);
            this.handlerScanner = handlerScanner;
        }

        public IMessaging Get()
        {
            if (messaging == null)
            {
                if (handlerScanner == null)
                {
                    handlerScanner = new HandlerScanner();
                    var assembly = System.Reflection.Assembly.GetCallingAssembly();
                    handlerScanner.ScanAssembly(assembly);
                }
                messaging = new MessagingCore(serviceProvider, handlerScanner);
            }
            return messaging;
        }
    }
}
