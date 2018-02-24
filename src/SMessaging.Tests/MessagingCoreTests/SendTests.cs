using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;
using SMessaging.Abstractions;
using SMessaging.Internal;

namespace SMessaging.Tests.MessagingCoreTests
{
    [TestFixture]
    public class SendTests
    {
        private readonly HandlerProvider handlerProvider = new HandlerProvider(new TestServiceProvider());
        private readonly HandlerScanner handlerScanner = new HandlerScanner().ScanAssembly(Assembly.GetExecutingAssembly());

        [Test]
        public async Task ShouldHandleMessage()
        {
            // Arrange
            var messagingCore = new MessagingCore(handlerProvider, handlerScanner);

            // Act
            var result = await messagingCore.Send(new TestMessageFirst());

            // Assert
            Assert.AreEqual(200, result.Code);
        }

        [Test]
        public void WhenHandlerNotFound_ThenThrowException()
        {
            // Arrange
            var messagingCore = new MessagingCore(handlerProvider, handlerScanner);

            // Act, Assert
            Assert.ThrowsAsync<MessagingInfrastructureException>(() => messagingCore.Send(new TestMessageSecond()));
        }

        [Test]
        public void WhenHandlerForMessageNotFound_ThenThrowException()
        {
            // Arrange
            var messagingCore = new MessagingCore(handlerProvider, handlerScanner);

            // Act, Assert
            Assert.ThrowsAsync<MessagingInfrastructureException>(() => messagingCore.Send(new TestMessageThird()));
        }
    }
}
