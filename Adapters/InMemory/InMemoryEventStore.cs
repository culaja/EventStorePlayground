using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Messaging;
using Ports.EventStore;

namespace InMemory
{
    public sealed class InMemoryEventStore : IEventStore
    {
        private readonly List<IDomainEvent> _allDomainEvents = new List<IDomainEvent>();
        
        public IDomainEvent Append(IDomainEvent domainEvent)
        {
            _allDomainEvents.Add(domainEvent);
            return domainEvent;
        }

        public IEnumerable<IDomainEvent> LoadAllFor<T>() where T : AggregateRoot
        {
            return _allDomainEvents
                .Where(domainEvent => domainEvent.AggregateRootType == typeof(T)).ToList();
        }
    }
}