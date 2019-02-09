using System;

namespace Common.Messaging
{
    public interface IDomainEvent : IMessage
    {
        Guid AggregateRootId { get; }

        Type AggregateRootType { get; }

        T ApplyTo<T>(T aggregateRoot) where T : AggregateRoot;
    }
}