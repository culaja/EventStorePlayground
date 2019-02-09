using System.Collections.Generic;
using Common.Messaging;

namespace Tests.IntegrationTests
{
    public sealed class NoOpMessageBus : IMessageBus
    {
        public IReadOnlyList<IMessage> DispatchAll(IReadOnlyList<IMessage> messages)
        {
            return messages;
        }

        public IMessage Dispatch(IMessage message)
        {
            return message;
        }
    }
}