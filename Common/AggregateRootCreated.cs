using System;
using System.Collections.Generic;

namespace Common
{
    public sealed class AggregateRootCreated : ValueObject<AggregateRootCreated>, IDomainEvent
    {
        public AggregateRootCreated(Guid aggregateRootId, Type aggregateRootType)
        {
            AggregateRootId = aggregateRootId;
            AggregateRootType = aggregateRootType;
        }

        public Guid AggregateRootId { get; }
        public Type AggregateRootType { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateRootId;
            yield return AggregateRootType;
        }
    }
}