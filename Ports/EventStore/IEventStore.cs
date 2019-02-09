using System.Collections.Generic;
using Common;
using Common.Messaging;

namespace Ports.EventStore
{
    public interface IEventStore
    {
        IDomainEvent Append(IDomainEvent domainEvent);

        IEnumerable<IDomainEvent> LoadAllForAggregateStartingFrom<T>(int position = 0) where T: AggregateRoot;
    }
}