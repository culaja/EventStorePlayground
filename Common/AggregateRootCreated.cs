using System;
using System.Collections.Generic;
using Common.Messaging;

namespace Common
{
    public abstract class AggregateRootCreated : ValueObject<AggregateRootCreated>, IDomainEvent
    {
        public Guid AggregateRootId { get; }
        public Type AggregateRootType { get; }
        public ulong Version { get; }

        protected AggregateRootCreated(
            Guid aggregateRootId,
            Type aggregateRootType,
            ulong version)
        {
            AggregateRootId = aggregateRootId;
            AggregateRootType = aggregateRootType;
            Version = version;
        }

        public abstract T ApplyTo<T>(T aggregateRoot) where T : AggregateRoot;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateRootId;
            yield return AggregateRootType;
            yield return Version;
        }
    }
}