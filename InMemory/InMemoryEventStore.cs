using System;
using System.Collections.Generic;
using Common;
using Ports;

namespace InMemory
{
    public sealed class InMemoryEventStore : IEventStore
    {
        private readonly List<IDomainEvent> _allDomainEvents = new List<IDomainEvent>();
        
        public void Append(IDomainEvent domainEvent)
        {
            _allDomainEvents.Add(domainEvent);
        }

        public IEnumerable<IDomainEvent> LoadAllStartingFrom(int position)
        {
            return _allDomainEvents.GetRange(position, _allDomainEvents.Count - position);
        }
    }
}