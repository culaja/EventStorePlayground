using System;
using System.Collections.Generic;

namespace Common.Messaging
{
    public abstract class DomainEvent<T> : ValueObject<DomainEvent<T>>, IDomainEvent
        where T : AggregateRoot
    {
        public Guid AggregateRootId { get; }
        public ulong Version { get; }

        public Type AggregateRootType => typeof(T);

        protected DomainEvent(Guid aggregateRootId, ulong version)
        {
            AggregateRootId = aggregateRootId;
            Version = version;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateRootId;
            yield return AggregateRootType;
            yield return Version;
        }
    }
}