using System;

namespace Common.Messaging
{
    public interface IDomainEvent : IMessage
    {
        Guid AggregateRootId { get; }

        string AggregateName { get; }
        
        ulong Version { get; }

        IDomainEvent SetVersion(ulong version);
    }
}