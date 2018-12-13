using System;
using System.Collections.Generic;

namespace Common
{
    public abstract class DomainEvent : ValueObject<DomainEvent>
    {
        public Guid AggregateRootId { get; }
        public abstract Type AggregateRootType { get; }

        protected DomainEvent(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateRootId;
            yield return AggregateRootType;
        }
    }
}