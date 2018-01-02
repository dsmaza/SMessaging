using System.Threading.Tasks;
using SMessaging.Abstractions;

namespace SMessaging.Tests.MessagingCoreTests
{
    public class TestMessageFirstHandler : IHandleMessage<TestMessageFirst>
    {
        public Task<MessageResult> Handle(TestMessageFirst message)
        {
            return Task.FromResult(new MessageResult(200, "Yeah"));
        }
    }
}
