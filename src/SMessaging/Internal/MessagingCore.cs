﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SMessaging.Abstractions;

[assembly: InternalsVisibleTo("SMessaging.Tests")]
namespace SMessaging.Internal
{
    class MessagingCore : IMessaging
    {
        private static readonly MethodInfo addMessageHandler = typeof(MessagingCore).GetMethod(nameof(AddMessageHandler), BindingFlags.Instance | BindingFlags.NonPublic);
        private readonly Dictionary<Type, Func<object, Task<MessageResult>>> messageHandlers = new Dictionary<Type, Func<object, Task<MessageResult>>>();
        private readonly IHandlerProvider handlerProvider;

        public MessagingCore(IHandlerProvider handlerProvider, HandlerScanner handlerScanner)
        {
            this.handlerProvider = handlerProvider ?? throw new ArgumentNullException(nameof(handlerProvider));
            RegisterHandlers(handlerScanner);
        }

        private void RegisterHandlers(HandlerScanner handlerScanner)
        {
            foreach (var item in handlerScanner)
            {
                var messageType = item.Key;
                var handlerType = item.Value;

                addMessageHandler
                    .MakeGenericMethod(new Type[] { messageType, handlerType })
                    .Invoke(this, new object[] { });
            }
        }

        private void AddMessageHandler<TMessage, THandler>()
            where THandler : class, IHandleMessage<TMessage>
        {
            messageHandlers.Add(typeof(TMessage),
                message => HandleMessage<TMessage, THandler>((TMessage)message));
        }

        private async Task<MessageResult> HandleMessage<TMessage, THandler>(TMessage message)
            where THandler : class, IHandleMessage<TMessage>
        {
            using (var scope = handlerProvider.CreateScope())
            {
                var handler = scope.GetService(typeof(THandler)) as THandler;
                if (handler == null)
                {
                    throw new MessagingInfrastructureException($"Handler '{typeof(THandler).Name}' not found");
                }

                return await handler.Handle(message);
            }
        }

        public async Task<MessageResult> Send<TMessage>(TMessage message)
        {
            Func<object, Task<MessageResult>> messageHandler;
            if (messageHandlers.TryGetValue(typeof(TMessage), out messageHandler))
            {
                return await messageHandler(message);
            }
            throw new MessagingInfrastructureException($"Handler for message '{typeof(TMessage).Name}' not found");
        }
    }
}
