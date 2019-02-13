using System;
using Shared.Common;

namespace Ports
{
    public interface IRemoteEventSubscriber
    {
        IRemoteEventSubscriber Register<T>(Action<SharedEvent> messageReceivedHandler) where T : IAggregateEventSubscription, new();
    }
}