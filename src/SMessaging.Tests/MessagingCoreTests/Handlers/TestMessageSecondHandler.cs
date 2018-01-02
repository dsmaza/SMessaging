using System.Threading.Tasks;
using SMessaging.Abstractions;

namespace SMessaging.Tests.MessagingCoreTests
{
    public class TestMessageSecondHandler : IHandleMessage<TestMessageSecond>
    {
        public Task<MessageResult> Handle(TestMessageSecond message)
        {
            return Task.FromResult(MessageResult.Null);
        }
    }
}
