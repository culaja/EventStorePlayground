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
        
        public static T CreateNewFrom<T>(params object[] aggregateRootParameters) where T : AggregateRoot
        {
            var newAggregate = (T)Activator.CreateInstance(typeof(T), aggregateRootParameters);
            newAggregate.Add(new AggregateRootCreated(typeof(T), aggregateRootParameters));
            return newAggregate;
        }
        
        public static T RestoreFrom<T>(params object[] aggregateRootParameters) where T : AggregateRoot
        {
            var newAggregate = (T)Activator.CreateInstance(typeof(T), aggregateRootParameters);
            return newAggregate;
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