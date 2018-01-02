using System.Threading.Tasks;

namespace SMessaging.Abstractions
{
    public interface IMessaging
    {
        Task<MessageResult> Send<TMessage>(TMessage message);
    }
}
