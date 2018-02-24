using System;

namespace SMessaging.Abstractions
{
    public interface IHandlerScope : IDisposable
    {
        object GetService(Type serviceType);
    }
}
