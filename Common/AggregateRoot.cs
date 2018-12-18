using System;
using System.Collections.Generic;
using Common.Eventing;
using static System.Guid;

namespace Common
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; }
        
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        
        protected AggregateRoot(params object[] aggregateRootParameters)
        {
            Id = (Guid)aggregateRootParameters[0];
            Add(new AggregateRootCreated(GetType(), aggregateRootParameters));
        }
        
        public static T CreateNewWith<T>(params object[] aggregateRootParameters) where T : AggregateRoot
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