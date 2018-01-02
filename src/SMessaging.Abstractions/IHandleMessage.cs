using System.Threading.Tasks;

namespace SMessaging.Abstractions
{
    public interface IHandleMessage<in TMessage>
    {
        Task<MessageResult> Handle(TMessage message);
    }
}
