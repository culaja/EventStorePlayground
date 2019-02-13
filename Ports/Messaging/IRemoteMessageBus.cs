using System;
using Common;
using Common.Messaging;

namespace Ports.Messaging
{
    public interface IRemoteMessageBus
    {
        IDomainEvent Send(IDomainEvent e);

        IRemoteMessageBus SubscribeTo<T, TK>(Action<TK> messageReceivedHandler)
            where T : AggregateRoot
            where TK : IDomainEvent;
    }
}