using System;
using Common.Messaging;
using Shared.Common;

namespace Ports
{
    public interface IRemoteEventSubscriber
    {
        IRemoteEventSubscriber Register<T>(Action<IDomainEvent> messageReceivedHandler) where T : IAggregateEventSubscription, new();
    }
}