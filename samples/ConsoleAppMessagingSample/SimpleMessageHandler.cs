using System.Threading.Tasks;
using SMessaging.Abstractions;

namespace ConsoleAppMessagingSample
{
    public class SimpleMessageHandler : IHandleMessage<SimpleMessage>
    {
        public Task<MessageResult> Handle(SimpleMessage message)
        {
            return Task.FromResult(new MessageResult(200, new { Text = "Hello World" }));
        }
    }
}
