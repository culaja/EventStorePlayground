using System;
using System.Collections.Generic;
using static System.Guid;

namespace Common
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; }
        
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
        public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

        protected AggregateRoot(Guid id)
        {
            Id = id;
        }

        public static TK CreateNew<TK>() where TK : AggregateRoot
        {
            var newAggregate = (TK)Activator.CreateInstance(typeof(TK), new object[] { NewGuid() });
            newAggregate.Add(new AggregateRootCreated(newAggregate.Id, typeof(TK)));
            return newAggregate;
        }

        protected DomainEvent Add(DomainEvent newDomainEvent)
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