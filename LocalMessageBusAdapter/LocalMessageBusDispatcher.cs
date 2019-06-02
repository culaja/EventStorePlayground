using System;
using System.Collections.Generic;
using Common;
using Common.Messaging;
using static Common.Nothing;

namespace LocalMessageBusAdapter
{
    internal sealed class LocalMessageBusDispatcher : ILocalMessageBus
    {
        private readonly Func<IMessage, Result> _messageHandlers;

        public LocalMessageBusDispatcher(Func<IMessage, Result> messageHandlers)
        {
            _messageHandlers = messageHandlers;
        }
        
        public Nothing DispatchAll(IReadOnlyList<IMessage> messages)
        {
            messages.Map(Dispatch);
            return NotAtAll;
        }

        public Nothing Dispatch(IMessage message) => _messageHandlers(message)
            .OnBoth(result => result.IsSuccess
                ? NotAtAll
                : WriteMessageHandlerError(message, result.Error));

        private Nothing WriteMessageHandlerError(IMessage message, string error)
        {
            Console.WriteLine($"[{nameof(LocalMessageBusDispatcher)}] {message.GetType().Name}: {error}");
            return NotAtAll;
        }
    }
}