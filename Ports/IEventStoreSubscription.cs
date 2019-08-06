using System;
using System.Collections.Generic;
using Common.Messaging;

namespace Ports
{
    public interface IEventStoreSubscription : IDisposable
    {
        string Name { get; }

        IEnumerable<IDomainEvent> Stream { get; }

        void Stop();
    }
}