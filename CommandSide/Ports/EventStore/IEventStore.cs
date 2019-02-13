using System.Collections.Generic;
using Common;
using Common.Messaging;

namespace Ports.EventStore
{
    public interface IEventStore
    {
        IDomainEvent Append(IDomainEvent domainEvent);

        IEnumerable<IDomainEvent> LoadAllFor<T>() where T: AggregateRoot;
    }
}