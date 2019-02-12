using System;
using System.Collections.Generic;
using Common.Messaging;

namespace Common
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; }
        
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        
        protected AggregateRoot(Guid id)
        {
            Id = id;
        }
        
        protected IDomainEvent Add(IDomainEvent newDomainEvent)
        {
            _domainEvents.Add(newDomainEvent);
            return newDomainEvent;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}