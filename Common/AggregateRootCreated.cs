using System;
using System.Collections.Generic;
using Common.Messaging;

namespace Common
{
    public abstract class AggregateRootCreated : ValueObject<AggregateRootCreated>, IDomainEvent
    {
        public Guid AggregateRootId { get; }
        public Type AggregateRootType { get; }
        public ulong Version { get; private set; }
        
        protected AggregateRootCreated(
            Guid aggregateRootId,
            Type aggregateRootType)
        {
            AggregateRootId = aggregateRootId;
            AggregateRootType = aggregateRootType;
        }
        
        public IDomainEvent SetVersion(ulong version)
        {
            (Version == 0).OnBoth(
                () => Version = version,
                () => throw new InvalidOperationException($"Version already set to {Version} and you want it to set it to {version}"));

            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateRootId;
            yield return AggregateRootType;
        }
    }
}