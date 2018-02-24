using System;
using System.Threading.Tasks;
using SMessaging;

namespace ConsoleAppMessagingSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
            Console.ReadLine();
        }

        static async Task Run()
        {
            var messaging = new MessagingProvider().Get();
            await messaging.Send(new SimpleMessage());
        }
    }
}
