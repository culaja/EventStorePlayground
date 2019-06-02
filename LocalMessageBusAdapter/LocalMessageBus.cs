using System;
using System.Collections.Generic;
using Common;
using Common.Messaging;

namespace LocalMessageBusAdapter
{
    public sealed class LocalMessageBus : ILocalMessageBus, IDisposable
    {
        private readonly LocalMessageBusWorker _worker;

        public LocalMessageBus(Func<IMessage, Result> messageHandlers)
        {
            _worker = new LocalMessageBusWorker(
                new LocalMessageBusDispatcher(messageHandlers));
        }

        public Nothing DispatchAll(IReadOnlyList<IMessage> messages) => _worker.DispatchAll(messages);

        public Nothing Dispatch(IMessage message) => _worker.Dispatch(message);

        public void Dispose() => _worker.Dispose();
    }
}