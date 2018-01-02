using System;

namespace SMessaging.Tests.MessagingCoreTests
{
    class TestServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(TestMessageFirstHandler))
            {
                return new TestMessageFirstHandler();
            }
            return null;
        }
    }
}
