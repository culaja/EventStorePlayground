using System;

namespace Common
{
    public sealed class AggregateRootCreated : DomainEvent
    {
        public AggregateRootCreated(Guid aggregateRootId, Type aggregateRootType) : base(aggregateRootId)
        {
            AggregateRootType = aggregateRootType;
        }

        public override Type AggregateRootType { get; }
    }
}