using System;
using System.Collections.Generic;
using static System.Guid;

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

        public static T CreateNew<T>() where T : AggregateRoot
            => (T) CreateNew(typeof(T));

        public static AggregateRoot CreateNew(Type type)
        {
            var newAggregate = (AggregateRoot)Activator.CreateInstance(type, NewGuid());
            newAggregate.Add(new AggregateRootCreated(newAggregate.Id, type));
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