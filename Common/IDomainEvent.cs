using System;

namespace Common
{
    public interface IDomainEvent
    {
        Guid AggregateRootId { get; }

        Type AggregateRootType { get; }

        T ApplyTo<T>(T aggregateRoot) where T : AggregateRoot;
    }
}