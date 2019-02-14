using System.Collections.Generic;
using Common.Messaging;
using Shared.Common;

namespace Ports.EventStore
{
    public interface IEventStore
    {
        IDomainEvent Append(IDomainEvent domainEvent);

        IEnumerable<IDomainEvent> LoadAllFor<T>() where T: IAggregateEventSubscription, new();
    }
}