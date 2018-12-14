using System.Collections.Generic;
using Common;

namespace Ports.EventStore
{
    public interface IEventStore
    {
        void Append(IDomainEvent domainEvent);

        IEnumerable<IDomainEvent> LoadAllStartingFrom<T>(int position = 0) where T: AggregateRoot;
    }
}