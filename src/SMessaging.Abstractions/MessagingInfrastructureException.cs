using System;
using System.Runtime.Serialization;

namespace SMessaging.Abstractions
{
    public class MessagingInfrastructureException : Exception
    {
        public MessagingInfrastructureException()
        {
        }

        public MessagingInfrastructureException(string message) : base(message)
        {
        }

        public MessagingInfrastructureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MessagingInfrastructureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
