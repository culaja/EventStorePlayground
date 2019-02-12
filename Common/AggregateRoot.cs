using System;
using System.Collections.Generic;
using Common.Messaging;

namespace Common
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; }
        protected ulong Version { get; private set; }
        
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        
        protected AggregateRoot(Guid id, ulong version)
        {
            Id = id;
            Version = version;
        }
        
        protected IDomainEvent Add(IDomainEvent newDomainEvent)
        {
            _domainEvents.Add(newDomainEvent);
            return newDomainEvent;
        }

        protected ulong IncrementedVersion() => ++Version;

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}