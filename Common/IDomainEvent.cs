using System;

namespace Common
{
    public interface IDomainEvent
    {
        Guid AggregateRootId { get; }

        Type AggregateRootType { get; }

        AggregateRoot ApplyTo(AggregateRoot aggregateRoot);
    }
}